using BattleSystem.Buff.Selector;
using Common;
using LivedObjectSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UISysyem;
using UnityEngine;

namespace BattleSystem.Buff
{
    public class BuffEffector
    {
        private BuffEffectType effectType;
        public BuffEffector(BuffEffectType effectType)
        {
            this.effectType = effectType;
        }

        public void Do(BuffData data, LivedObject enemy)
        {
            if (!GameHelper.MeetOdds(data.cfg.Odds))
            {
                Debug.Log($"不满足概率{(float)data.cfg.Odds / 10000}%");
                return;
            }


            List<LivedObject> targets = GetTarget(data);
            Effect(data, targets);
        }

        private List<LivedObject> GetTarget(BuffData data)
        {
            List<LivedObject> list = new List<LivedObject>();
            bool isHero = data.owner is HeroData;
            // 选择主目标
            switch (data.cfg.TargetMainType)
            {
                case TargetMainType.None:
                    break;
                case TargetMainType.Friend:
                    list = BattleMgr.Inst.CurRoomData.GetAllLivedObjs(isHero);
                    break;
                case TargetMainType.Enemy:
                    list = BattleMgr.Inst.CurRoomData.GetAllLivedObjs(!isHero);
                    break;
                case TargetMainType.All:
                    list = BattleMgr.Inst.CurRoomData.GetAllLivedObjs();
                    break;
                case TargetMainType.Max:
                    break;
            }

            // 选中类型
            switch (data.cfg.SelectType)
            {
                case SelectType.None:
                    break;
                case SelectType.Select:

                    break;
                case SelectType.Fixed:
                    list = BattleMgr.Inst.CurRoomData.GetAllLivedObjs(new Pos(data.cfg.FixedPosX, data.cfg.FixedPosY), data.cfg.UnitType, isHero);
                    break;
                case SelectType.Random:
                    list = list.GetRandomData(data.cfg.Random, RandomType.Ignore);
                    break;
                case SelectType.Self:
                    list.Clear();
                    string[] strArr = data.cfg.SelfPosType.ToString().Split(',');
                    for (int i = 0; i < strArr.Length; i++)
                    {
                        SelfPosType type = (SelfPosType)Enum.Parse(typeof(SelfPosType), strArr[i]);
                        GridData gridData = BattleMgr.Inst.CurRoomData.GetGridData((Pos)data.owner.Pos, isHero, type);

                        if (gridData != null && gridData.Data != null)
                            list.Add(gridData.Data);
                    }
                    break;

                case SelectType.Max:
                    break;
            }

            return list;
        }

        IEnumerator SelectTargetByManual()
        {
            Debug.Log(1);
            yield return new WaitUntil(() => UIMgr.Inst.HasSelect);
            Debug.Log(2);
        }

        private void Effect(BuffData data, List<LivedObject> targets)
        {
            string totalName = $"BattleSystem.Buff.Effect.{effectType}";
            Type type = Type.GetType(totalName);
            BuffEffect effect = (BuffEffect)type.Assembly.CreateInstance(totalName);
            if (effect == null)
                Debug.LogError($"没有对应实例{effectType},请编写");
            else
                effect.Do(data, targets);
        }

        private bool CheckCondition()
        {
            return false;
        }
    }
}

using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BattleSystem
{
    public class RoundData
    {
        public RoundOwnerType Type { get; set; }
        public bool IsEnd { get; set; }
        public Dictionary<BuffTgrType, BuffData> BuffTrgDic = new Dictionary<BuffTgrType, BuffData>();
        public RoundData()
        {

        }

        // 回合开始
        public void RoundStart()
        {
            switch (Type)
            {
                case RoundOwnerType.None:
                case RoundOwnerType.Monster:
                    Type = RoundOwnerType.Player;
                    BattleMgr.Inst.CurRoomData.TgrGridsBuff(true, BuffTgrType.SelfRoundStart);
                    BattleMgr.Inst.ResetHeorAttack();
                    break;
                case RoundOwnerType.Player:
                    Type = RoundOwnerType.Monster;
                    BattleMgr.Inst.CurRoomData.TgrGridsBuff(false, BuffTgrType.SelfRoundStart);
                    BattleMgr.Inst.MonsterStartRound();
                    break;
            }

            Debug.Log($"{GetOwnerStr()}回合开始");
        }

        // 回合结束
        public void RoundEnd()
        {
            Debug.Log($"{GetOwnerStr()}回合结束");
            switch (Type)
            {
                case RoundOwnerType.Player:
                    BattleMgr.Inst.CurRoomData.TgrGridsBuff(true, BuffTgrType.SelfRoundEnd);
                    break;
                case RoundOwnerType.Monster:
                    BattleMgr.Inst.CurRoomData.TgrGridsBuff(false, BuffTgrType.SelfRoundEnd);
                    break;
            }
            RoundStart();
        }

        // 获取当前回合主人字符串
        private string GetOwnerStr()
        {
            string str = string.Empty;
            switch (Type)
            {
                case RoundOwnerType.Player:
                    str = "玩家";
                    break;
                case RoundOwnerType.Monster:
                    str = "怪物";
                    break;
            }

            return str;
        }
    }
}

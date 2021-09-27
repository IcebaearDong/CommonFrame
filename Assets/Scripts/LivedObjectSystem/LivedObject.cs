using BattleSystem;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivedObjectSystem
{
    public class LivedObject
    {
        public Dictionary<BuffTgrType, List<BuffData>> BuffTrgDic;
        public int MaxHp { get; set; }
        private int _curHP;
        public int CurHp
        {
            get { return _curHP; }
            set
            {
                _curHP = Math.Min(MaxHp, value);
            }
        }
        public int Attack { get; set; }
        // 攻击次数
        public int AttackCount { get; set; }
        // 攻击过的次数
        public int AttackedCount { get; set; }
        public bool IsAction { get; set; }
        public bool IsCritical { get; set; }

        public Pos? Pos { get; set; }

        public bool IsDead { get; set; }

        // 被攻击的敌人
        public LivedObject AttackedEnemy { get; set; }
        public LivedObject()
        {
            BuffTrgDic = new Dictionary<BuffTgrType, List<BuffData>>();
        }
        public void Attacked(LivedObject other)
        {
            TgrBuff(BuffTgrType.SelfAttackedStart, other);
            int damage = other.Attack;
            if (other.IsCritical)
                damage *= 2;

            CurHp -= damage;
            if (CurHp <= 0)
                OnDead();

            TgrBuff(BuffTgrType.SelfAttackedEnd, other);
        }

        public virtual void Attacking(LivedObject target)
        {
            AttackedEnemy = target;
        }

        public void ResetAction()
        {
            IsAction = false;
        }

        // 当死亡
        public virtual void OnDead()
        {
            TgrBuff(BuffTgrType.SelfDeadStart);
            IsDead = true;
            TgrBuff(BuffTgrType.SelfAttackedEnd);
            EventMgr.Dispatch(EventsType.HeroDead);
        }

        // 触发Buff
        public void TgrBuff(BuffTgrType type, LivedObject enemy = null)
        {
            if (!BuffTrgDic.ContainsKey(type)) return;

            var list = BuffTrgDic[type];
            foreach (var buff in list)
            {
                buff.Effect(enemy);
            }
        }

        // 添加Buff
        public void AddBuff(int buffID)
        {
            BuffData buffData = new BuffData(buffID, this);
            BuffTgrType type = buffData.cfg.TriggerStage;
            if (BuffTrgDic.ContainsKey(type))
            {
                BuffTrgDic[type].Add(buffData);
            }
            else
            {
                List<BuffData> list = new List<BuffData>();
                list.Add(buffData);
                BuffTrgDic[type] = list;
            }
        }
        // 移除Buff
        public void RemoveBuff(int id)
        {

        }
    }
}

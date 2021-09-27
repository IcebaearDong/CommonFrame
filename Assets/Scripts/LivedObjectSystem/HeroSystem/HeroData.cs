using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using LivedObjectSystem;
using BattleSystem;


public class HeroData : LivedObject
{
    public HeroExcelItem Cfg { get; set; }
    public int ID { get => Cfg.id; }

    public HeroData(int id)
    {
        Cfg = ExlMgr.Inst.GetHeroCfg(id);
        Attack = Cfg.Attack;
        MaxHp = Cfg.Hp;
        CurHp = Cfg.Hp;
        Attack = Cfg.Attack;
        AttackCount = Cfg.AttackCount;
        AddBuff(Cfg.PasstiveBuff);
    }

    public HeroData()
    {

    }

    public override void Attacking(LivedObject target)
    {
        base.Attacking(target);
        for (int i = 0; i < AttackCount; i++)
        {
            TgrBuff(BuffTgrType.SelfAttackStart, target);
            target.Attacked(this);

            AttackedReset();
            TgrBuff(BuffTgrType.SelfAttackEnd, target);
        }
    }

    public bool IsInGrid()
    {
        return Pos != null;
    }

    public void ResetHero()
    {
        Pos = null;
        IsAction = false;
    }

    // 攻击后重置
    private void AttackedReset()
    {
        IsAction = true;
        AttackedCount++;
        IsCritical = false;
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSystem;
using Common;
using LivedObjectSystem;
using TimeSystem;
using UnityEngine;

public class MonsterData : LivedObject
{
    public MonsterExcelItem Cfg { get; set; }
    public int ID { get => Cfg.id; }
    public MonsterData(int id)
    {
        Cfg = ExlMgr.Inst.GetMonsterCfg(id);
        MaxHp = Cfg.HP;
        CurHp = Cfg.HP;
        Attack = Cfg.Attack;
        AttackCount = Cfg.AttackCount;
    }

    public MonsterData()
    {

    }

    public override void Attacking(LivedObject target = null)
    {
        IsAction = false;
        for (int i = 0; i < AttackCount; i++)
        {
            TimeMgr.Inst.AddTask(1000, 1, DoAttack);
        }
    }

    private void DoAttack()
    {
        HeroData heroData = GetAttackTarget();
        if (heroData == null)
        {
            Debug.Log("怪物没有找到目标");
            return;
        }

        base.Attacking(heroData);

        // Buff加攻击力
        // 攻击
        heroData.Attacked(this);

        // 结束
        IsAction = true;

        EventMgr.Dispatch(EventsType.MonsterAttackEnd);
        EventMgr.Dispatch(EventsType.UpdateMonsterPanel);
    }

    // 怪物获取目标
    public HeroData GetAttackTarget()
    {
        int row = Cst.Get(CstType.GridRow);
        HeroData data = null;
        for (int i = 0; i < row; i++)
        {
            data = GetOneRowRandomHero(i);
            if (data != null) break;
        }

        return data;
    }

    // 获取单行随机一个英雄
    private HeroData GetOneRowRandomHero(int row)
    {
        int column = Cst.Get(CstType.GridColumn);
        GridData[,] grids = GameMgr.Inst.GameData.GetCurRoomData().Grids;
        List<GridData> list = new List<GridData>();
        for (int i = 0; i < column; i++)
        {
            GridData data = grids[i, row];
            list.Add(data);
        }

        list = list.FindAll(t => t.Data != null && !t.Data.IsDead);
        if (list.Count == 0) return null;
        return (HeroData)list.GetRandomData().Data;
    }

}


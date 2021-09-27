using Common;
using LivedObjectSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BattleSystem
{
    public class RoomData
    {
        public RoomType type;
        public MonsterData Monster { get; set; }
        public bool IsFirstIn = true;
        // 玩家英雄格子
        public GridData[,] Grids { get; set; }
        // 怪物格子
        public GridData[,] MonsterGrids { get; set; }
        public bool IsStart { get; set; }
        public MapExcelItem Cfg { get; set; }
        public List<int> SellHeros { get; set; }

        private Queue<MonsterData> attackQueue;
        public RoomData(MapExcelItem cfg)
        {
            Cfg = cfg;
            type = cfg.Type;
            switch (cfg.Type)
            {
                case RoomType.None:
                    break;
                case RoomType.Monster:
                case RoomType.Elite:
                case RoomType.Boss:
                    Grids = CreateGridData();
                    MonsterGrids = CreateMonsterGridData();
                    attackQueue = new Queue<MonsterData>();
                    break;
                case RoomType.Shop:
                    break;
                case RoomType.Event:
                    break;
            }
        }
        public RoomData()
        {

        }

        // 创建随机商店英雄
        private List<int> CreateShopHeros()
        {
            // 剩余英雄(所有英雄-玩家拥有英雄)
            List<HeroData> list = GameMgr.Inst.GameData.OwnHeros.Values.ToList();
            List<HeroExcelItem> allList = ExlMgr.Inst.GetAllHeroCfg().ToList();
            foreach (var item in list)
                allList.Remove((HeroExcelItem)item.Cfg);

            allList = allList.GetRandomData(Cst.Get(CstType.ShopIntCount), RandomType.Ignore);
            List<int> res = new List<int>();
            foreach (var item in allList)
                res.Add(item.id);

            return res;
        }

        // 横向遍历
        public void ForeachGrid<T>(Action<GridData> action, GridData[,] target)
        {
            int row = Cst.Get(CstType.GridRow);
            int column = Cst.Get(CstType.GridColumn);
            int allCount = row * column;
            List<GridData> list = new List<GridData>();
            for (int i = 0; i < allCount; i++)
            {
                GridData grid = target[i % row, i / column];
                list.Add(grid);
            }

            list.ForEach(action);
        }

        // 横向获取
        public List<LivedObject> GetAllLivedObjs(bool isHero)
        {
            GridData[,] grids = isHero ? Grids : MonsterGrids;
            List<LivedObject> list = new List<LivedObject>();
            ForeachGrid<GridData>(t =>
            {
                if (t.Data != null && !t.Data.IsDead)
                    list.Add(t.Data);
            }, grids);

            return list;
        }

        // 横向获取
        public List<LivedObject> GetAllLivedObjs()
        {
            List<LivedObject> list = new List<LivedObject>();
            list.AddRange(GetAllLivedObjs(true));
            list.AddRange(GetAllLivedObjs(false));

            return list;
        }

        // 获取范围内所有单位
        public List<LivedObject> GetAllLivedObjs(Pos pos, UnitType type, bool isHero = true)
        {
            List<LivedObject> list = new List<LivedObject>();
            int row = Cst.Get(CstType.GridRow);
            int col = Cst.Get(CstType.GridRow);
            int y = pos.Y;
            int x = pos.X;
            switch (type)
            {
                case UnitType.None:
                    break;
                case UnitType.Single:
                    list.Add(GetLivedObj(pos, isHero));
                    break;
                case UnitType.Row:
                    for (int i = 0; i < col; i++)
                    {
                        LivedObject obj = GetLivedObj(new Pos(i, y), isHero);
                        if (obj != null)
                            list.Add(obj);
                    }
                    break;
                case UnitType.Column:
                    for (int i = 0; i < row; i++)
                    {
                        LivedObject obj = GetLivedObj(new Pos(x, i), isHero);
                        if (obj != null)
                            list.Add(obj);
                    }
                    break;
                case UnitType.Around:
                    break;
                case UnitType.Max:
                    break;
            }

            return list;
        }

        // 获取单位
        public LivedObject GetLivedObj(Pos pos, bool isHero = true)
        {
            GridData gridData = GetGridData(pos, isHero);
            if (gridData == null) return null;

            return gridData.Data;
        }

        // 获得格子数据
        public GridData GetGridData(Pos pos, bool isHero = true)
        {
            GridData[,] grids = isHero ? Grids : MonsterGrids;
            if (pos.X >= 0 && pos.X < Cst.Get(CstType.GridColumn) && pos.Y >= 0 && pos.Y < Cst.Get(CstType.GridRow))
                return grids[pos.X, pos.Y];

            return null;
        }
        // 获取格子数据
        public GridData GetGridData(Pos selfPos, bool isHero, SelfPosType type)
        {
            Pos newPos = GetNewPos(selfPos, type);
            return GetGridData(newPos, isHero);
        }

        // 自身位置和相对位置获得新的位置
        public Pos GetNewPos(Pos selfPos, SelfPosType type)
        {
            Pos newPos = selfPos;
            string str = type.ToString();
            for (int i = 0; i < str.Length; i++)
            {
                char s = str[i];
                switch (s)
                {
                    case 'L':
                        newPos.X -= 1;
                        break;
                    case 'R':
                        newPos.X += 1;
                        break;
                    case 'T':
                        newPos.Y -= 1;
                        break;
                    case 'B':
                        newPos.Y += 1;
                        break;
                }
            }

            return newPos;
        }
        public void IntoRoom()
        {
            if (IsFirstIn)
            {
                IsFirstIn = false;
                GameMgr.Inst.GameData.ResetRoom();
                if (type == RoomType.Shop && SellHeros == null)
                    SellHeros = CreateShopHeros();
                GameMgr.Inst.SaveGame();
            }
        }

        // 创建格子数据
        private GridData[,] CreateGridData()
        {
            int row = Cst.Get(CstType.GridRow);
            int column = Cst.Get(CstType.GridColumn);
            GridData[,] grids = new GridData[row, column];
            for (int i = 0; i < grids.Length; i++)
            {
                GridData grid = new GridData();
                grid.Pos = new Pos(i % 3, i / 3);
                grids[i % 3, i / 3] = grid;
            }

            return grids;
        }

        // 创建怪物格子数据
        private GridData[,] CreateMonsterGridData()
        {
            GridData[,] grids = CreateGridData();
            int idx = 0;
            foreach (var grid in grids)
            {
                FieldInfo info = Cfg.GetType().GetField($"Pos{idx}");
                idx++;
                if (info != null)
                {
                    int id = (int)info.GetValue(Cfg);
                    if (id != 0)
                    {
                        grid.Data = new MonsterData(id);
                    }
                }
            }

            return grids;
        }
        // 房间战斗开始
        public void RoomBattleStart()
        {
            IsStart = true;
            EventMgr.AddListener(EventsType.MonsterAttackEnd, OnSingleMonsterAttackEnd);
            EventMgr.AddListener(EventsType.HeroDead, OnHeroDead);
        }

        // 房间战斗结束
        public void RoomBattleEnd()
        {
            IsStart = false;
            EventMgr.RemoveListener(EventsType.MonsterAttackEnd, OnSingleMonsterAttackEnd);
            EventMgr.RemoveListener(EventsType.HeroDead, OnHeroDead);
        }

        // 所有怪物开始攻击
        public void AllMonsterStartAttack()
        {
            attackQueue.Clear();
            ForeachGrid<GridData>(t =>
            {
                if (t.Data != null && t.Data is MonsterData)
                {
                    t.Data.ResetAction();
                    attackQueue.Enqueue((MonsterData)t.Data);
                }

            }, MonsterGrids);

            SingleMonsterAttack();
        }

        // 单个怪物攻击
        private void SingleMonsterAttack()
        {
            if (attackQueue.Count != 0)
            {
                MonsterData data = attackQueue.Dequeue();
                data.Attacking();
            }
        }

        // 单个怪物攻击结束
        private void OnSingleMonsterAttackEnd()
        {
            if (attackQueue.Count == 0)
            {
                OnAllMonsterAttackEnd();
                return;
            }

            SingleMonsterAttack();
        }

        // 所有怪物攻击结束
        private void OnAllMonsterAttackEnd()
        {
            EventMgr.Dispatch(EventsType.AllMonsterAttackEnd);
        }

        // 当英雄死亡
        private void OnHeroDead()
        {
            if (!IsHeroActive())
                BattleMgr.Inst.LoseRoom();
        }

        // 是否还有英雄存活
        public bool IsHeroActive()
        {
            bool res = false;
            foreach (var grid in Grids)
            {
                if (grid.Data != null && !grid.Data.IsDead)
                {
                    res = true;
                    break;
                }
            }

            return res;
        }

        // 触发回合Buff
        public void TgrGridsBuff(bool isHero, BuffTgrType type)
        {
            GridData[,] grids = isHero ? Grids : MonsterGrids;
            ForeachGrid<GridData>(t =>
            {
                if (t.Data != null)
                    t.Data.TgrBuff(type);
            }, grids);
        }

    }
}

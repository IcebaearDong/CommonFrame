using LivedObjectSystem;
using PlayerSystem;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSystem;
using Common;
using UISysyem;
using GameSystem;

namespace BattleSystem
{
    public class BattleMgr : Singleton<BattleMgr>
    {
        private GameData _gameData;
        private Dictionary<int, HeroData> IntHeroDic { get; set; }
        public RoundData? CurRoundData { get => _gameData.GetCurRoundData(); }
        public RoomData? CurRoomData { get => _gameData.GetCurRoomData(); }
        public BattleMgr()
        {
            IntHeroDic = new Dictionary<int, HeroData>();
            StartRandomHeros();

            EventMgr.AddListener(EventsType.AllMonsterAttackEnd, OnAllMonsterAttackEnd);
        }

        public Dictionary<int, HeroData> GetIntHero()
        {
            return IntHeroDic;
        }

        // 创建地图
        public List<RoomData> CreateMap()
        {
            List<RoomData> res = new List<RoomData>();
            List<MapExcelItem> list = ExlMgr.Inst.GetAllMapCfg().ToList();
            foreach (var item in list)
                res.Add(new RoomData(item));

            return res;
        }

        // 开始随机化英雄
        public void StartRandomHeros()
        {
            List<HeroExcelItem> list = ExlMgr.Inst.GetAllHeroCfg().ToList();
            int count = 3;
            IntHeroDic.Clear();
            for (int i = 0; i < count; i++)
            {
                HeroExcelItem cfg = list[UnityEngine.Random.Range(0, list.Count)];
                IntHeroDic.Add(cfg.id, new HeroData(cfg.id));
                list.Remove(cfg);
            }
        }

        // 房间获胜
        public void WinRoom()
        {
            CurRoomData.RoomBattleEnd();
            GameMgr.Inst.WinRoom();
            BroadcastMgr.Inst.AddBroadcast("玩家胜利");
        }

        // 房间失败
        public void LoseRoom()
        {
            CurRoomData.RoomBattleEnd();
            GameMgr.Inst.LoseGame();
            BroadcastMgr.Inst.AddBroadcast("玩家失败");
        }

        // 开始战斗
        public void StartBattle(GameData data)
        {
            _gameData = data;
            UIMgr.Inst.OpenPanel(PanelEnum.MapPanel);
        }

        // 进入房间
        public void IntoRoom(RoomData data, int idx)
        {
            if (CanIntoRoom(idx))
                DoIntoRoom(data);
        }

        // 是否可以进入房间
        public bool CanIntoRoom(int i)
        {
            return i == _gameData.mapIdx;
        }

        // 进入房间
        private void DoIntoRoom(RoomData data)
        {
            data.IntoRoom();
            UIMgr.Inst.IntoRoom(data);
        }

        // 购买英雄
        public void Buy(int id)
        {
            int heroMoney = ExlMgr.Inst.GetHeroCfg(id).Money;

            if (heroMoney > _gameData.curMoney)
            {
                BroadcastMgr.Inst.AddBroadcast("钱不够");
                return;
            }

            _gameData.curMoney -= heroMoney;
            _gameData.OwnHeros.Add(id, new HeroData(id));
            EventMgr.Dispatch(EventsType.SucBuyHero);
        }

        public bool IsRoomStart()
        {
            if (CurRoomData != null)
                return CurRoomData.IsStart;

            return false;
        }

        // 房间开始战斗
        public void RoomBattleStart()
        {
            CurRoomData.RoomBattleStart();
            _gameData.NewCurRoundData();
            RoundStart();
            EventMgr.Dispatch(EventsType.UpdateMonsterPanel);
        }

        public bool IsRoundEnd()
        {
            if (CurRoundData != null)
                return CurRoundData.IsEnd;

            return false;
        }

        // 怪物回合开始
        public void MonsterStartRound()
        {
            CurRoomData.AllMonsterStartAttack();
        }

        // 重置英雄攻击
        public void ResetHeorAttack()
        {
            List<int> list = new List<int>();
            CurRoomData.ForeachGrid<GridData>(t =>
            {
                if (t.Data != null)
                {
                    t.Data.ResetAction();
                }
            }, CurRoomData.Grids);
        }

        // 获取地图列表
        public List<RoomData> GetMapList()
        {
            return _gameData.MapList;
        }

        // 回合开始
        public void RoundStart()
        {
            _gameData.CurRoundData.RoundStart();
        }

        // 回合结束
        public void RoundEnd()
        {
            _gameData.CurRoundData.RoundEnd();

            EventMgr.Dispatch(EventsType.UpdateMonsterPanel);
        }

        // 所有怪物攻击结束
        private void OnAllMonsterAttackEnd()
        {
            CurRoundData.RoundEnd();
        }
    }
}

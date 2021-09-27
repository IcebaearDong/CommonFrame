using BattleSystem;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem
{
    // 游戏主数据
    public class GameData
    {
        public bool isStartGame = false;
        public Dictionary<int, HeroData> OwnHeros { get; set; }
        public List<int> WaitHeros;
        public List<RoomData> MapList { get; set; }
        public int? mapIdx { get; set; }
        public byte volum = 50;
        public int curHP = Cst.Get(CstType.IntHP);
        public int curMoney = Cst.Get(CstType.IntMoney);
        public GameLanguageType lageType = GameLanguageType.ZH;
        public RoundData? CurRoundData { get; set; }

        // 获取当前房间数据
        public RoomData GetCurRoomData()
        {
            if (mapIdx != null) return MapList[(int)mapIdx];

            return null;
        }

        // 重置地图索引
        public void ResetMapIdx()
        {
            mapIdx = 0;
        }

        // 获取当前回合数据
        public RoundData GetCurRoundData()
        {
            return CurRoundData;
        }

        public void NewCurRoundData()
        {
            CurRoundData = new RoundData();
        }

        // 地图索引加一
        public void WinRoom()
        {
            if (mapIdx == null)
                Debug.LogError("mapIdx为空");

            if (mapIdx >= Cst.Get(CstType.MaxRoomCount))
            {
                Debug.Log("游戏通关");
                isStartGame = false;
                OwnHeros = null;
                WaitHeros = null;
                MapList = null;
                mapIdx = null;
                EventMgr.Dispatch(EventsType.PassGame);
                return;
            }

            mapIdx++;
            ResetRoom();
        }

        // 重置所有英雄
        private void ResetOwnHeros()
        {
            foreach (var item in OwnHeros.Values)
                item.ResetHero();
        }

        // 重置等待区英雄
        private void ResetWaitHeros()
        {
            WaitHeros.Clear();
            foreach (var item in OwnHeros.Values)
                WaitHeros.Add(item.ID);
        }

        // 重置房间
        public void ResetRoom()
        {
            ResetOwnHeros();
            ResetWaitHeros();
            EventMgr.Dispatch(EventsType.WinRoom);
        }

        // 英雄是否被玩家购买
        public bool IsHeroBuyed(int heroID)
        {
            if (GetCurRoomData().SellHeros == null)
                return false;

            return GetCurRoomData().SellHeros.Contains(heroID) && OwnHeros.ContainsKey(heroID);
        }
    }
}

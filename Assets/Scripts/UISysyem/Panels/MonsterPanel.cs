using BattleSystem;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UISysyem.Components;
using UnityEngine;
using UnityEngine.UI;

namespace UISysyem.Panels
{
    class MonsterPanel : IPanel
    {
        public override PanelType Type => PanelType.FullScreen;

        // 地图数据
        private RoomData _roomData;
        private Button _btnReturn;
        private Button _btnStart;
        private GridLayoutGroup _heroGrid;
        // 英雄地图格子
        private GridLayoutGroup _grid;
        // 怪物地图格子
        private GridLayoutGroup _monsterGrid;
        private GameObject _waitGroup;
        private Button _btnRoundEnd;
        public override void OnAwake()
        {
            _btnReturn = gameObject.FindComponent<Button>("BtnReturn");
            _btnStart = gameObject.FindComponent<Button>("BtnStart");
            _heroGrid = gameObject.FindComponent<GridLayoutGroup>("HeroScroll/Viewport/Grid");
            _grid = gameObject.FindComponent<GridLayoutGroup>("RoomMapGrid");
            _monsterGrid = gameObject.FindComponent<GridLayoutGroup>("MonsterMapGrid");
            _waitGroup = gameObject.FindObject("HeroScroll");
            _btnRoundEnd = gameObject.FindComponent<Button>("BtnRoundEnd");

            UIEventListener.Get(_btnReturn.gameObject).onClick = OnReturn;
            UIEventListener.Get(_btnStart.gameObject).onClick = OnRoomStart;
            UIEventListener.Get(_btnRoundEnd.gameObject).onClick = OnRoundEnd;
        }
        public override void OnStart(params object[] args)
        {
            if (args != null && args.Length > 0)
                _roomData = (RoomData)args[0];

            ShowPanel();
        }

        public override void OnUpdate()
        {

        }

        public override void OnClose()
        {

        }

        public override void AddEvents()
        {
            EventMgr.AddListener(EventsType.UpdateMonsterPanel, ShowPanel);
        }

        public override void RemoveEvents()
        {
            EventMgr.RemoveListener(EventsType.UpdateMonsterPanel, ShowPanel);
        }

        private void ShowPanel()
        {
            ShowGrids(_monsterGrid.gameObject, false);
            ShowStartBtn();
            ShowGrids(_grid.gameObject);
            ShowWaitAreaHero();
            ShowRoundEndBtn();
        }

        private void OnReturn(GameObject obj)
        {
            UIMgr.Inst.OpenPanel(PanelEnum.StartGamePanel);
        }

        // 显示等待区域英雄
        private void ShowWaitAreaHero()
        {
            _waitGroup.Show(!_roomData.IsStart);
            if (_roomData.IsStart)
                return;

            int idx = 0;
            foreach (var data in GameMgr.Inst.GameData.OwnHeros.Values.Where(t => !t.IsInGrid()))
            {
                GameObject uiItem = idx < _heroGrid.transform.childCount ? _heroGrid.transform.GetChild(idx).gameObject :
                    ResMgr.Inst.LoadUICompoent(UIPrefabType.Items, "HeroItem", PanelEnum.BattlePanel);

                if (idx >= _heroGrid.transform.childCount)
                    uiItem.SetParent(_heroGrid.gameObject, Vector3.one);

                Binder.Bind(uiItem, null, data);
                uiItem.GetOrAddCompoent<HeroComponent>();

                UIMgr.Inst.ShowHeroItem(uiItem, data);
                idx++;
            }

            UIMgr.Inst.HideExtraItem(_heroGrid.gameObject, idx);
        }

        // 显示格子
        private void ShowGrids(GameObject grid, bool isHero = true)
        {
            int column = Cst.Get(CstType.GridColumn);
            int row = Cst.Get(CstType.GridRow);
            int allCount = row * column;

            for (int i = 0; i < allCount; i++)
            {
                GameObject uiItem = i < grid.transform.childCount ? grid.transform.GetChild(i).gameObject : ResMgr.Inst.LoadUICompoent(UIPrefabType.Items, "RoomMapGridItem", PanelEnum.MonsterPanel);

                if (i >= grid.transform.childCount)
                    uiItem.gameObject.SetParent(grid.gameObject, Vector3.zero);

                GridData[,] grids = isHero ? _roomData.Grids : _roomData.MonsterGrids;

                if (isHero)
                    uiItem.GetOrAddCompoent<GridComponent>().Init(grids[i % column, i / row]);

                ShowGridItem(uiItem, grids[i % column, i / row]);
            }
        }

        // 房间开始
        private void OnRoomStart(GameObject btn)
        {
            BattleMgr.Inst.RoomBattleStart();
        }

        // 显示玩家地图格子
        private void ShowGridItem(GameObject obj, GridData data)
        {
            if (data == null)
            {
                print("格子数据为空");
                return;
            }

            Binder.Bind(obj, null, data);
            obj.FindComponent<Text>("Pos").text = $"{data.Pos.X},{data.Pos.Y}";
            obj.FindObject("LivedObjItem").Show(data.Data != null);

            if (data.Data != null)
            {
                //heroUIItem.GetOrAddCompoent<HeroComponent>();
                //Binder.Bind(heroUIItem, null, data.Data);
                UIMgr.Inst.ShowLivedObjItem(obj.FindObject("LivedObjItem"), data.Data);
            }
        }

        private void ShowStartBtn()
        {
            _btnStart.gameObject.Show(!_roomData.IsStart);
        }

        // 显示回合结束按钮
        private void ShowRoundEndBtn()
        {
            _btnRoundEnd.gameObject.Show(BattleMgr.Inst.IsRoomStart() && BattleMgr.Inst.CurRoundData.Type == RoundOwnerType.Player && !BattleMgr.Inst.IsRoundEnd());

        }

        // 回合结束
        private void OnRoundEnd(GameObject obj)
        {
            BattleMgr.Inst.RoundEnd();
        }
    }
}

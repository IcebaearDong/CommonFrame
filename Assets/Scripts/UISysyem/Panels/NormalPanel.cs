using BattleSystem;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UISysyem.Panels
{
    class NormalPanel : IPanel
    {
        public override PanelType Type => PanelType.FullScreen;

        // 地图数据
        private RoomData _roomData;
        private Text _labTitle;
        private Button _btnReturn;
        private Button _btnWin;
        private Text _curLab;

        // 商店Grid
        private GridLayoutGroup _shopGrid;
        public override void OnAwake()
        {
            _labTitle = gameObject.FindComponent<Text>("LabTitle");
            _btnReturn = gameObject.FindComponent<Button>("BtnReturn");
            _btnWin = gameObject.FindComponent<Button>("BtnWin");
            _curLab = gameObject.FindComponent<Text>("CurLab");
            _shopGrid = gameObject.FindComponent<GridLayoutGroup>("ShopGrid");
            UIEventListener.Get(_btnReturn.gameObject).onClick = OnReturn;
            UIEventListener.Get(_btnWin.gameObject).onClick = OnWin;

            EventMgr.AddListener(EventsType.SucBuyHero, ShowPanel);
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

        private void ShowPanel()
        {
            _labTitle.text = _roomData.type.ToString();
            _curLab.text = $"{Lage.Get(LageType.CurMoney)}{GameMgr.Inst.GameData.curMoney}";

            _shopGrid.gameObject.Show(_roomData.type == RoomType.Shop);
            if (_roomData.type == RoomType.Shop)
                ShowShopGrid();
        }

        private void OnReturn(GameObject obj)
        {
            UIMgr.Inst.OpenPanel(PanelEnum.StartGamePanel);
        }

        private void OnWin(GameObject btn)
        {
            BattleMgr.Inst.WinRoom();
        }

        // 显示商店出售英雄
        private void ShowShopGrid()
        {
            if (_roomData.SellHeros == null) return;

            for (int i = 0; i < _roomData.SellHeros.Count; i++)
            {
                GameObject uiItem = i < _shopGrid.transform.childCount ? _shopGrid.transform.GetChild(i).gameObject : ResMgr.Inst.LoadUICompoent(UIPrefabType.Items, "HeroItem", PanelEnum.NormalPanel);
                int id = _roomData.SellHeros[i];
                HeroExcelItem cfg = ExlMgr.Inst.GetHeroCfg(id);

                if (i >= _shopGrid.transform.childCount)
                    uiItem.SetParent(_shopGrid.gameObject, Vector3.zero);

                UIMgr.Inst.ShowShopHeroItem(uiItem, cfg);
                Binder.Bind(uiItem, OnBuy, cfg);
            }

            UIMgr.Inst.HideExtraItem(_shopGrid.gameObject, _roomData.SellHeros.Count);
        }

        // 购买
        private void OnBuy(GameObject obj)
        {
            HeroExcelItem cfg = Binder.Get<HeroExcelItem>(obj);
            BattleMgr.Inst.Buy(cfg.id);
        }
    }
}

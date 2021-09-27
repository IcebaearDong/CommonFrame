using BattleSystem;
using Common;
using PlayerSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

namespace UISysyem.Panels
{
    class BattlePanel : IPanel
    {
        public override PanelType Type => PanelType.FullScreen;
        // 主玩家
        private Player _player;
        // 英雄格子
        private GridLayoutGroup _heroGrid;
        // 确认按钮
        private Button _btnConfirm;
        // 再次随机化按钮
        private Button _btnRandom;

        public override void OnAwake()
        {
            _heroGrid = gameObject.FindComponent<GridLayoutGroup>("HeroGrid");
            _btnConfirm = gameObject.FindComponent<Button>("BtnConfirm");
            _btnRandom = gameObject.FindComponent<Button>("BtnRandom");

            UIEventListener.Get(_btnConfirm.gameObject).onClick = OnConfirm;
            UIEventListener.Get(_btnRandom.gameObject).onClick = OnRandom;
        }

        public override void OnStart(params object[] args)
        {
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
            //ShowMonster();
            //ShowHeroGrid();
            //ShowCard();
            ShowHeroGrid();
        }

        // 显示英雄格子
        private void ShowHeroGrid()
        {
            int idx = 0;
            var intHeroDic = BattleMgr.Inst.GetIntHero();
            foreach (var data in intHeroDic.Values)
            {
                GameObject uiItem = idx < _heroGrid.transform.childCount ? _heroGrid.transform.GetChild(idx).gameObject :
                    ResMgr.Inst.LoadUICompoent(UIPrefabType.Items, "HeroItem", PanelEnum.BattlePanel);

                if (idx >= _heroGrid.transform.childCount)
                    uiItem.SetParent(_heroGrid.gameObject, Vector3.one);

                UIMgr.Inst.ShowHeroItem(uiItem, data);
                idx++;
            }
        }

        // 确认
        private void OnConfirm(GameObject obj)
        {
            GameMgr.Inst.StartGame();
        }

        // 再次随机化
        private void OnRandom(GameObject btn)
        {
            BattleMgr.Inst.StartRandomHeros();
            ShowHeroGrid();
        }
    }
}

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
    // 开始游戏页面
    class StartGamePanel : IPanel
    {
        public override PanelType Type => PanelType.FullScreen;

        private GridLayoutGroup _grid;
        public override void OnAwake()
        {
            _grid = gameObject.FindComponent<GridLayoutGroup>("Grid");
        }
        public override void OnStart(params object[] args)
        {


            if (_grid.transform.childCount < (int)StartPanelBtnType.Max) return;

            for (StartPanelBtnType i = StartPanelBtnType.NewGame; i < StartPanelBtnType.Max; i++)
            {
                GameObject uiItem = _grid.transform.GetChild((int)i).gameObject;
                Binder.Bind(uiItem, OnClick, i);
                if (i == StartPanelBtnType.ContinueGame)
                    uiItem.Show(GameMgr.Inst.GameData.isStartGame);
            }
        }

        public override void OnUpdate()
        {

        }

        public override void OnClose()
        {

        }

        // 点击按钮
        private void OnClick(GameObject btn)
        {
            StartPanelBtnType type = Binder.Get<StartPanelBtnType>(btn);
            switch (type)
            {
                case StartPanelBtnType.NewGame:
                    GameMgr.Inst.NewGame();
                    break;
                case StartPanelBtnType.ContinueGame:
                    GameMgr.Inst.ContinueGame();
                    break;
                case StartPanelBtnType.Settings:
                    GameMgr.Inst.SetGame();
                    break;
                case StartPanelBtnType.Exit:
                    GameMgr.Inst.QuitGame();
                    break;
                case StartPanelBtnType.Max:
                    break;
            }
        }
    }
}

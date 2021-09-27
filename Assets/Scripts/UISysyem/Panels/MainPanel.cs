using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using PlayerSystem;
using SceneSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UISysyem.Panels
{
    class MainPanel : IPanel
    {
        public override PanelType Type => PanelType.FullScreen;

        private InputField _labName;
        // 主玩家
        private Player _player;
        // 改名按钮
        private Button _btnChangeName;
        // 战斗按钮
        private Button _btnFight;
        public override void OnAwake()
        {
            _labName = gameObject.FindComponent<InputField>("LabName");
            _btnChangeName = gameObject.FindComponent<Button>("BtnChangeName");
            _btnFight = gameObject.FindComponent<Button>("BtnFight");

            _player = PlayerManager.Inst.GetMainPlayer();
            _btnChangeName.onClick.AddListener(OnChangeName);
            _btnFight.onClick.AddListener(OnFight);
        }

        public override void OnStart(params object[] args)
        {


            ShowPanel();

            //EventMgr.AddListener(EventsType.ChangeName, ShowPanel);
        }

        public override void OnUpdate()
        {
        }

        public override void OnClose()
        {

        }


        private void ShowPanel()
        {
            _labName.text = _player.name;
        }

        // 更改名字
        private void OnChangeName()
        {
            string nameToChange = _labName.text;
            if (GameHelper.IsNullString(nameToChange, "名字") || !GameHelper.IsSafeString(nameToChange))
                return;

            PlayerManager.Inst.ChangeName(nameToChange);
        }

        // 战斗
        private void OnFight()
        {
            SceneMgr.Inst.ChangeScene(new BattleScene());
        }

    }
}

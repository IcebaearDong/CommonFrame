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
        // �����
        private Player _player;
        // ������ť
        private Button _btnChangeName;
        // ս����ť
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

        // ��������
        private void OnChangeName()
        {
            string nameToChange = _labName.text;
            if (GameHelper.IsNullString(nameToChange, "����") || !GameHelper.IsSafeString(nameToChange))
                return;

            PlayerManager.Inst.ChangeName(nameToChange);
        }

        // ս��
        private void OnFight()
        {
            SceneMgr.Inst.ChangeScene(new BattleScene());
        }

    }
}

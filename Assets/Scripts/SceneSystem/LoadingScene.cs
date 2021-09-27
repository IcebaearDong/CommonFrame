using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using UISysyem;
using UnityEngine;

namespace SceneSystem
{
    class LoadingScene : IScene
    {
        public override SceneType SceneType => SceneType.LoadingScene;

        public override void OnStart()
        {
            GameMgr.Inst.Init();
            UIMgr.Inst.OpenPanel(PanelEnum.ToplayerPanel);
            UIMgr.Inst.OpenPanel(PanelEnum.LoadingPanel);

            // TODO加载资源,加载完成后回调打开开始场景

        }

        public override void OnEnd()
        {
            // 释放资源

        }

        public override void OnUpdate()
        {

        }


        private void Init()
        {

        }
    }
}


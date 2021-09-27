using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UISysyem;

namespace SceneSystem
{
    class StartScene : IScene
    {
        public override SceneType SceneType => SceneType;
        public override void OnStart()
        {
            UIMgr.Inst.OpenPanel(PanelEnum.StartGamePanel);
        }

        public override void OnUpdate()
        {

        }

        public override void OnEnd()
        {

        }
    }
}

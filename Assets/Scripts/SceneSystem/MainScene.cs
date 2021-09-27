using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UISysyem;

namespace SceneSystem
{
    class MainScene : IScene
    {
        public override SceneType SceneType => SceneType.MainScene;

        public override void OnStart()
        {
            UIMgr.Inst.OpenPanel(PanelEnum.MainPanel);
        }

        public override void OnUpdate()
        {

        }

        public override void OnEnd()
        {

        }
    }
}

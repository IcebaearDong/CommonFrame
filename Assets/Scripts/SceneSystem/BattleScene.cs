using BattleSystem;
using Common;
using SceneSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UISysyem;

namespace SceneSystem
{
    class BattleScene : IScene
    {
        public override SceneType SceneType => SceneType.BattleScene;

        public override void OnStart()
        {
            if (GameMgr.Inst.GameData.isStartGame)
                UIMgr.Inst.OpenPanel(PanelEnum.MapPanel);
            else
                UIMgr.Inst.OpenPanel(PanelEnum.BattlePanel);
        }

        public override void OnUpdate()
        {

        }


        public override void OnEnd()
        {

        }
    }
}

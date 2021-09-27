using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;
using SceneSystem;
using UISysyem;
using Common;

public class LoginScene : IScene
{
    public override SceneType SceneType => SceneType.LoginScene;

    public override void OnStart()
    {
        UIMgr.Inst.OpenPanel( PanelEnum.LoginPanel );
    }

    public override void OnUpdate()
    {
    }

    public override void OnEnd()
    {
    }
}


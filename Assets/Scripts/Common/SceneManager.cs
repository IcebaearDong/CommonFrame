using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;
using Common;
using SceneSystem;
using UISysyem;
using Net;
using TimeSystem;

public class SceneMgr : Singleton<SceneMgr>
{
    private IScene _curScene;

    public void ChangeScene(IScene scene)
    {
        if (_curScene != null)
            _curScene.OnEnd();

        _curScene = scene;
        _curScene.OnStart();
    }

    public void DoUpdate()
    {
        TimeMgr.Inst.Update();
        _curScene.OnUpdate();
        UIMgr.Inst.OnUpdate();
    }

}


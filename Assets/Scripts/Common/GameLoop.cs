using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;
using SceneSystem;
using UISysyem;
using Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using BattleSystem.Buff;
using TimeSystem;
using Common;
using System.Threading;
using System.Collections;

public class GameLoop : MonoBehaviour
{
    private void Start()
    {

        DontDestroyOnLoad(this);
        SceneMgr.Inst.ChangeScene(new LoadingScene());
    }

    private void Update()
    {
        SceneMgr.Inst.DoUpdate();
    }
}


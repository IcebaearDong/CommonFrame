using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;
using Common;

namespace SceneSystem
{
    public abstract class IScene
    {
        public abstract SceneType SceneType { get; }

        public abstract void OnStart();
        public abstract void OnUpdate();
        public abstract void OnEnd();
    }
}



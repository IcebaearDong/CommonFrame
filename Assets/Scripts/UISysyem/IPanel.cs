using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using UnityEngine;

namespace UISysyem
{
    public abstract class IPanel : MonoBehaviour
    {
        public abstract PanelType Type { get; }
        public abstract void OnAwake();
        public abstract void OnStart(params object[] args);
        public abstract void OnUpdate();
        public abstract void OnClose();
        // 添加事件
        public virtual void AddEvents()
        {

        }
        // 移除事件
        public virtual void RemoveEvents()
        {

        }
    }
}

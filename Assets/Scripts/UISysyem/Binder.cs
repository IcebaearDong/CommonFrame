using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UISysyem
{
    class Binder : MonoBehaviour
    {
        private object[] objects;
        public static void Bind(GameObject go, Action<GameObject> action, params object[] args)
        {
            Binder binder = go.GetComponent<Binder>();
            if (binder == null) binder = go.AddComponent<Binder>();
            binder.objects = args;

            if (action != null)
                UIEventListener.Get(go).onClick = action;
        }

        public static T Get<T>(GameObject go, int idx = 0)
        {
            Binder biner = go.GetComponent<Binder>();
            if (biner != null)
            {
                object[] objects = biner.objects;
                return (T)objects[idx];
            }

            return default;
        }
    }
}

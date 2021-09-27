using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UISysyem.Components
{
    public interface IDropable
    {
        public void OnDrop(GameObject obj);
    }
}

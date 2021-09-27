using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net;
using UnityEngine;

namespace UISysyem
{
    public class PanelData
    {
        public CanvasGroup canvasGroup;
        public IPanel scrPanel;
        public GameObject gObj;
        public PanelData( IPanel scrPanel, GameObject gObj, CanvasGroup canvas )
        {
            this.scrPanel = scrPanel;
            this.gObj = gObj;
            this.canvasGroup = canvas;
        }
    }
}

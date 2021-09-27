using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Net;
using UnityEngine.UI;

namespace UISysyem
{
    class LoadingPanel : IPanel
    {
        public override PanelType Type => PanelType.FullScreen;
        private Slider _slider;

        private ushort prograss;
        public override void OnAwake()
        {

        }

        public override void OnStart(params object[] args)
        {
            _slider = gameObject.FindComponent<Slider>("Slider");
        }


        public override void OnUpdate()
        {
            if (prograss == 150)
            {
                UIMgr.Inst.OpenPanel(PanelEnum.StartGamePanel);
                return;
            }

            prograss++;
            _slider.value = (float)prograss / 150;
        }

        public override void OnClose()
        {

        }
    }
}


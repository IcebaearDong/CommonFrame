using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DG.Tweening;
using UI.Panels;
using UISysyem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels
{
    class TopLayerPanel : IPanel
    {
        public override PanelType Type => PanelType.TopLayer;
        // 广播组
        private GameObject _broadcastGroup;
        public override void OnAwake()
        {
            _broadcastGroup = transform.Find("BroadcastGroup").gameObject;
            EventMgr.AddListener<string>(EventsType.SendBroadcast, OnSendBroadcast);
        }
        public override void OnStart(params object[] args)
        {

        }

        public override void OnUpdate()
        {

        }

        public override void OnClose()
        {

        }

        // 发送广播
        private void OnSendBroadcast(string msg)
        {
            GameObject item = ResMgr.Inst.LoadUICompoent(UIPrefabType.Items, "BroadcastItem");
            item.transform.SetParent(_broadcastGroup.transform, false);
            item.transform.Find("Lab").GetComponent<Text>().text = msg;
            Tween tweMoveY = item.transform.DOLocalMoveY(40, 0.5f);
            Tween tween = item.GetComponent<Image>().DOFade(0, 0.5f).SetDelay(1.5f).OnComplete(() => { Destroy(item); });
            item.transform.Find("Lab").GetComponent<Text>().DOFade(0, 0.5f).SetDelay(1.5f);

            if (_broadcastGroup.transform.childCount > 0)
            {
                for (int i = 0; i < _broadcastGroup.transform.childCount; ++i)
                {
                    GameObject child = _broadcastGroup.transform.GetChild(_broadcastGroup.transform.childCount - 1 - i).gameObject;
                    Vector3 pos = child.transform.localPosition;
                    if (pos.y > Screen.height / 2)
                    {
                        Destroy(child);
                        continue;
                    }

                    pos.y = 50 + (i * 50);
                    child.transform.DOLocalMoveY(pos.y + 50, 0.5f);
                }
            }
        }
    }
}

using BattleSystem;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UISysyem.Components
{
    public class HeroComponent : EventTrigger
    {
        // 初始的父物体
        private GameObject _orParent;
        // 面板物体
        private GameObject _panel;
        // 开始拖拽
        public override void OnBeginDrag(PointerEventData eventData)
        {
            HeroData data = Binder.Get<HeroData>(gameObject);
            if (data != null && data.IsAction)
            {
                print("已经行动过");
                return;
            }

            if (_orParent == null)
                _orParent = transform.parent.gameObject;

            if (_panel == null)
                _panel = GetComponentInParent<IPanel>().gameObject;

            transform.SetParent(_panel.transform);
            transform.SetAsLastSibling();
            //gameObject.GetComponentsInChildren<Graphic>().ToList().ForEach(t => t.raycastTarget = false);
        }

        // 拖拽中
        public override void OnDrag(PointerEventData eventData)
        {
            gameObject.transform.position = eventData.position;
        }

        // 结束拖拽
        public override void OnEndDrag(PointerEventData eventData)
        {
            List<GameObject> objs = UIMgr.Inst.GetOverObjs();
            GameObject curObj = objs.Find(t => t.GetComponent<IDropable>() != null);
            if (curObj != null)
            {
                // 逻辑层
                curObj.GetComponent<IDropable>().OnDrop(gameObject);
            }

            // UI层
            gameObject.SetParent(_orParent, Vector3.zero);

            // 事件派发
            if (GameMgr.Inst.GameData.isStartGame)
                EventMgr.Dispatch(EventsType.UpdateMonsterPanel);
        }

    }
}

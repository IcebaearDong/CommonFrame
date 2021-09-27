using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace UISysyem.Components
{
    public class ExchangeCardNode : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public bool isDebug;
        Image image;

        void Start()
        {
            image = GetComponent<Image>();

        }

        void Update()
        {
            if (isDebug)
            {
                print("全局" + gameObject.transform.position);
                print("局部" + gameObject.transform.position);
            }
        }

        // ------

        Vector2 dragOffset;
        Transform lastParent;

        public void OnBeginDrag(PointerEventData eventData)
        {
            lastParent = transform.parent;
            print(eventData.position);
            dragOffset = (Vector2)transform.position - eventData.position;
            transform.position = eventData.position + dragOffset;
            image.raycastTarget = false;

            transform.SetParent(transform.parent.parent);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position + dragOffset;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            float moveTime = 0.3f;

            GameObject exchangeObj = eventData.pointerCurrentRaycast.gameObject;

            ExchangeCardNode exchangeNode = null;
            if (exchangeObj != null)
                exchangeNode = exchangeObj.GetComponent<ExchangeCardNode>();

            if (exchangeNode != null)
            {
                transform.SetParent(exchangeObj.transform.parent);
                exchangeObj.transform.SetParent(lastParent);

                exchangeNode.MoveToPos(Vector3.zero, moveTime);
            }
            else
            {
                transform.SetParent(lastParent);
            }
            MoveToPos(Vector3.zero, moveTime);

            image.raycastTarget = true;
        }

        public void MoveToPos(Vector3 targetPos, float time)
        {
            transform.DOKill();
            if (time <= 0)
            {
                transform.localPosition = targetPos;
            }
            else
            {
                transform.DOLocalMove(targetPos, time, false);
            }
        }

    }
}

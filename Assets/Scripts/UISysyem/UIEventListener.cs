using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace UISysyem
{
    public class UIEventListener : EventTrigger
    {
        public delegate void VoidDelegate(GameObject go);
        public delegate void BoolDelegate(GameObject go, bool isValue);
        public delegate void FloatDelegate(GameObject go, float fValue);
        public delegate void IntDelegate(GameObject go, int iIndex);
        public delegate void StringDelegate(GameObject go, string strValue);

        public VoidDelegate onSubmit;
        public Action<GameObject> onClick;
        public Action<GameObject, PointerEventData> onDrag;
        public BoolDelegate onHover;
        public BoolDelegate onToggleChanged;
        public FloatDelegate onSliderChanged;
        public FloatDelegate onScrollbarChanged;
        public IntDelegate onDrapDownChanged;
        public StringDelegate onInputFieldChanged;

        public override void OnSubmit(BaseEventData eventData)
        {
            if (onSubmit != null)
                onSubmit(gameObject);
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (onHover != null)
                onHover(gameObject, true);
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            if (onClick != null)
            {
                Button btn = GetComponent<Button>();
                if (btn != null && !btn.interactable)
                    return;

                onClick(gameObject);
            }
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            if (onHover != null)
                onHover(gameObject, false);
        }
        public override void OnDrag(PointerEventData eventData)
        {
            //if (onSliderChanged != null)
            //    onSliderChanged(gameObject, gameObject.GetComponent<Slider>().value);
            //if (onScrollbarChanged != null)
            //    onScrollbarChanged(gameObject, gameObject.GetComponent<Scrollbar>().value);

            if (onDrag != null)
            {
                onDrag(gameObject, eventData);
            }

        }
        public override void OnSelect(BaseEventData eventData)
        {
            if (onDrapDownChanged != null)
                onDrapDownChanged(gameObject, gameObject.GetComponent<Dropdown>().value);
        }
        public override void OnUpdateSelected(BaseEventData eventData)
        {
            if (onInputFieldChanged != null)
                onInputFieldChanged(gameObject, gameObject.GetComponent<InputField>().text);
        }
        public override void OnDeselect(BaseEventData eventData)
        {
            if (onInputFieldChanged != null)
                onInputFieldChanged(gameObject, gameObject.GetComponent<InputField>().text);
        }

        public static UIEventListener Get(GameObject go)
        {
            UIEventListener listener = go.GetComponent<UIEventListener>();
            if (listener == null) listener = go.AddComponent<UIEventListener>();
            return listener;
        }
    }
}

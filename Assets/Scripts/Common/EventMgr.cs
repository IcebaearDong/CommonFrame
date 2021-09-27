using System;
using System.Collections.Generic;
namespace Common
{
    public class EventMgr
    {
        static private Dictionary<EventsType, Delegate> eventLst = new Dictionary<EventsType, Delegate>();

        #region ¼àÌý
        public static void AddListener(EventsType eventID, Action eventHandler)
        {
            if (eventLst.ContainsKey(eventID))
                eventLst[eventID] = (Action)eventLst[eventID] + eventHandler;
            else
                eventLst.Add(eventID, eventHandler);
        }

        public static void AddListener<T>(EventsType eventID, Action<T> eventHandler)
        {
            if (eventLst.ContainsKey(eventID))
                eventLst[eventID] = (Action<T>)eventLst[eventID] + eventHandler;
            else
                eventLst.Add(eventID, eventHandler);
        }

        public static void AddListener<T1, T2>(EventsType eventID, Action<T1, T2> eventHandler)
        {
            if (eventLst.ContainsKey(eventID))
                eventLst[eventID] = (Action<T1, T2>)eventLst[eventID] + eventHandler;
            else
                eventLst.Add(eventID, eventHandler);
        }
        #endregion

        #region È¡Ïû¼àÌý
        public static void RemoveListener(EventsType eventID, Action eventHandler)
        {
            if (!eventLst.ContainsKey(eventID))
                return;

            eventLst[eventID] = (Action)eventLst[eventID] - eventHandler;
        }

        public static void RemoveListener<T>(EventsType eventID, Action<T> eventHandler)
        {
            if (!eventLst.ContainsKey(eventID))
                return;

            eventLst[eventID] = (Action<T>)eventLst[eventID] - eventHandler;
        }

        public static void RemoveListener<T1, T2>(EventsType eventID, Action<T1, T2> eventHandler)
        {
            if (!eventLst.ContainsKey(eventID))
                return;

            eventLst[eventID] = (Action<T1, T2>)eventLst[eventID] - eventHandler;
        }
        #endregion

        #region ÅÉ·¢
        public static void Dispatch(EventsType eventID)
        {
            Delegate d = null;
            if (!eventLst.TryGetValue(eventID, out d))
                return;
            if (d == null)
                return;

            Delegate[] callBacks = d.GetInvocationList();
            for (int i = 0; i < callBacks.Length; i++)
            {
                Action callBack = callBacks[i] as Action;
                if (callBack == null)
                    continue;

                callBack();
            }
        }

        public static void Dispatch<T>(EventsType eventID, T arg1)
        {
            Delegate d = null;
            if (!eventLst.TryGetValue(eventID, out d))
                return;
            if (d == null)
                return;

            Delegate[] callBacks = d.GetInvocationList();
            for (int i = 0; i < callBacks.Length; i++)
            {
                Action<T> callBack = callBacks[i] as Action<T>;
                if (callBack == null)
                    continue;

                callBack(arg1);
            }
        }

        public static void Dispatch<T1, T2>(EventsType eventID, T1 arg1, T2 arg2)
        {
            Delegate d = null;
            if (!eventLst.TryGetValue(eventID, out d))
                return;
            if (d == null)
                return;

            Delegate[] callBacks = d.GetInvocationList();
            for (int i = 0; i < callBacks.Length; i++)
            {
                Action<T1, T2> callBack = callBacks[i] as Action<T1, T2>;
                if (callBack == null)
                    continue;

                callBack(arg1, arg2);
            }
        }
        #endregion 

    }
}

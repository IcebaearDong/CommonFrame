using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> where T : new()
{
    private static T inst;
    public static T Inst
    {
        get
        {
            if (inst == null)
            {
                inst = new T();
                return inst;
            }

            return inst;
        }
    }
}

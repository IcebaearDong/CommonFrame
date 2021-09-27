using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _inst;
    public static T Inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = FindObjectOfType<T>();
                if (_inst == null)
                {
                    GameObject gObj = new GameObject(typeof(T).Name);
                    _inst = gObj.AddComponent<T>();
                    GameObject gameLoop = GameObject.FindGameObjectWithTag("GameLoop");
                    if (gameLoop != null)
                        gObj.transform.SetParent(gameLoop.transform);

                    _inst.Init();
                }
            }

            return _inst;
        }
    }

    protected virtual void Init() { }
}

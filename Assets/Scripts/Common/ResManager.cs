using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections;
using Common;

public class ResMgr : MonoSingleton<ResMgr>
{
    private Hashtable cache;

    protected override void Init()
    {
        cache = new Hashtable();
    }

    private void DestroyRes()
    {
        //TODO
    }

    private void GetRes(string resName)
    {
        //TODO
    }

    public T LoadResource<T>(string path) where T : UnityEngine.Object
    {
        if (cache.ContainsKey(path))
            return cache[path] as T;

        T tRources = Resources.Load<T>(path);
        if (tRources == null)
            Debug.Log(string.Format("Can't find,Please Check{0}", path));
        else
            cache.Add(path, tRources);
        return tRources;
    }

    public GameObject LoadUICompoent(UIPrefabType type, string name, PanelEnum panel = PanelEnum.None)
    {
        StringBuilder sb = new StringBuilder("UI/");
        sb.Append(type.ToString()).Append("/").Append(name);
        string pathName = sb.ToString();

        GameObject obj = LoadResource<GameObject>(pathName);
        if (obj == null) Debug.LogError($"{pathName}ЮЊПе");
        GameObject clone = Instantiate<GameObject>(obj);
        return clone;
    }

}



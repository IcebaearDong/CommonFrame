using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ExcelDataBase<T> : ScriptableObject where T : ExcelItemBase
{
    public T[] items;
    private Dictionary<int, T> dic;

    public T GetExcelItem(int targetId)
    {
        if (dic == null)
        {
            dic = new Dictionary<int, T>();
            foreach (var item in items)
            {
                dic.Add(item.id, item);
            }
        }

        T t;
        dic.TryGetValue(targetId, out t);
        if (t == null)
            Debug.Log($"{targetId}’“≤ªµΩ∏√Id");

        return t;
    }

    public T[] GetExcelItems()
    {
        return items;
    }
}

public class ExcelItemBase
{
    public int id;
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    static class GameHelper
    {
        public static bool IsSafeString(string str, bool isBroadcast = true)
        {
            bool isSafty = !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
            if (!isSafty && isBroadcast)
                BroadcastMgr.Inst.AddBroadcast("含有非法字符");

            return isSafty;
        }

        public static bool IsNullString(string str, string prefix = "", bool isBroadcast = true)
        {
            bool isNull = string.IsNullOrEmpty(str);
            if (isNull && prefix != string.Empty && isBroadcast)
                BroadcastMgr.Inst.AddBroadcast($"{prefix}不能为空");

            return isNull;
        }

        public static void SaveJsonToFile<T>(T t)
        {
            var setting = new JsonSerializerSettings();
            setting.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            setting.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            string path = $"{Application.persistentDataPath }/{typeof(T).Namespace}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Debug.Log($"CreateFile--{path}");
            }

            File.WriteAllText($"{path}/{typeof(T).Name}.json", JsonConvert.SerializeObject(t, setting));
        }

        public static T ReadJsonFromFile<T>()
        {
            var setting = new JsonSerializerSettings();
            setting.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            setting.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            string path = $"{Application.persistentDataPath }/{typeof(T).Namespace}";
            return JsonConvert.DeserializeObject<T>(File.ReadAllText($"{path}/{typeof(T).Name}.json"), setting);
        }

        public static bool MeetOdds(int odds)
        {
            int random = UnityEngine.Random.Range(1, 11);
            return random <= odds;
        }

        public static int GetPerDamage(int val, int per)
        {
            int damage = Mathf.FloorToInt(val * (float)per / 100);
            return damage;
        }

        #region 拓展方法

        public static void SetParent(this GameObject obj, GameObject parent, Vector3 pos, Vector2 size = default, int scale = 1)
        {
            obj.transform.SetParent(parent.transform);
            obj.transform.localPosition = pos;
            obj.transform.localScale = Vector3.one * scale;

            if (size != default)
            {
                GridLayoutGroup grid = parent.GetComponent<GridLayoutGroup>();
                RectTransform rect = obj.GetComponent<RectTransform>();
                if (grid == null && rect != null)
                {
                    Vector2 half = new Vector2(0.5f, 0.5f);
                    rect.anchorMin = half;
                    rect.anchorMax = half;
                    rect.pivot = half;
                    rect.anchoredPosition = Vector2.zero;
                    rect.sizeDelta = size;
                }
            }
        }

        public static void Show(this GameObject obj, bool state)
        {
            obj.SetActive(state);
        }

        public static GameObject FindObject(this GameObject obj, string name)
        {
            Transform t = obj.transform.Find(name);
            if (t == null)
            {
                Debug.Log($"Can't find {name}");
                return null;
            }

            return t.gameObject;
        }

        public static T FindComponent<T>(this GameObject obj, string name) where T : UnityEngine.Object
        {
            return obj.FindObject(name).GetComponent<T>();
        }

        public static T GetOrAddCompoent<T>(this GameObject obj, Action Init = null) where T : Component
        {
            T comp = obj.GetComponent<T>();
            if (comp == null)
                return obj.AddComponent<T>();

            return comp;
        }

        // 获取随机数据
        public static T GetRandomData<T>(this List<T> list)
        {
            if (list == null || list.Count == 0) return default;

            int rIdx = UnityEngine.Random.Range(0, list.Count);
            return list[rIdx];
        }

        // 获取随机不重复的数据
        public static List<T> GetRandomData<T>(this List<T> list, int count, RandomType type)
        {
            switch (type)
            {
                case RandomType.None:
                    break;
                case RandomType.DontRepeat:
                    if (count > list.Count)
                    {
                        Debug.LogWarning($"随机的数量{count}超过数组数量{list.Count}");
                        return null;
                    }
                    break;
                case RandomType.Ignore:
                    count = Math.Min(count, list.Count);
                    break;
                default:
                    break;
            }


            List<T> res = new List<T>();
            for (int i = 0; i < count; i++)
            {
                int rIdx = UnityEngine.Random.Range(0, list.Count);
                T data = list[rIdx];
                res.Add(data);
                list.Remove(data);
                if (list.Count == 0) break;
            }

            return res;
        }

        // 检查标志位
        public static bool CheckFlag(int t, int count)
        {
            int flag = 1 << count;
            return (t & flag) != 0;
        }
        #endregion
    }
    // 文本
    static class Lage
    {
        public static string Get(LageType type)
        {
            LanguageExcelItem cfg = ExlMgr.Inst.GetLanguageCfg((int)type);
            switch (GameMgr.Inst.GameData.lageType)
            {
                case GameLanguageType.ZH:
                    return cfg.ZH;
                case GameLanguageType.EN:
                    return cfg.EN;
                default:
                    return cfg.ZH;
            }
        }
    }
    // 常量值
    public static class Cst
    {
        public static int Get(CstType type)
        {
            ConstantExcelItem cfg = ExlMgr.Inst.GetConstantCfg((int)type);
            return cfg.value;
        }
    }
}

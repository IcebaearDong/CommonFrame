/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using Common;

[Serializable]
public class MonsterExcelItem : ExcelItemBase
{
	public string Name;
	public int Attack;
	public int AttackCount;
	public int HP;
}

[CreateAssetMenu(fileName = "MonsterExcelData", menuName = "Excel To ScriptableObject/Create MonsterExcelData", order = 1)]
public class MonsterExcelData : ExcelDataBase<MonsterExcelItem>
{
}

#if UNITY_EDITOR
public class MonsterAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		MonsterExcelItem[] items = new MonsterExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			MonsterExcelItem item = new MonsterExcelItem();
			item.id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			item.Name = allItemValueRowList[i]["Name"];
			item.Attack = Convert.ToInt32(allItemValueRowList[i]["Attack"]);
			item.AttackCount = Convert.ToInt32(allItemValueRowList[i]["AttackCount"]);
			item.HP = Convert.ToInt32(allItemValueRowList[i]["HP"]);
			items[i] = item;
		}
		MonsterExcelData excelDataAsset = ScriptableObject.CreateInstance<MonsterExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(MonsterExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif



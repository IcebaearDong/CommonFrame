/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using Common;

[Serializable]
public class HeroExcelItem : ExcelItemBase
{
	public string Name;
	public int Attack;
	public int AttackCount;
	public int Hp;
	public int Money;
	public int PasstiveBuff;
}

[CreateAssetMenu(fileName = "HeroExcelData", menuName = "Excel To ScriptableObject/Create HeroExcelData", order = 1)]
public class HeroExcelData : ExcelDataBase<HeroExcelItem>
{
}

#if UNITY_EDITOR
public class HeroAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		HeroExcelItem[] items = new HeroExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			HeroExcelItem item = new HeroExcelItem();
			item.id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			item.Name = allItemValueRowList[i]["Name"];
			item.Attack = Convert.ToInt32(allItemValueRowList[i]["Attack"]);
			item.AttackCount = Convert.ToInt32(allItemValueRowList[i]["AttackCount"]);
			item.Hp = Convert.ToInt32(allItemValueRowList[i]["Hp"]);
			item.Money = Convert.ToInt32(allItemValueRowList[i]["Money"]);
			item.PasstiveBuff = Convert.ToInt32(allItemValueRowList[i]["PasstiveBuff"]);
			items[i] = item;
		}
		HeroExcelData excelDataAsset = ScriptableObject.CreateInstance<HeroExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(HeroExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif



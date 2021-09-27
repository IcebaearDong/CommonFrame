/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using Common;

[Serializable]
public class LanguageExcelItem : ExcelItemBase
{
	public string ZH;
	public string EN;
}

[CreateAssetMenu(fileName = "LanguageExcelData", menuName = "Excel To ScriptableObject/Create LanguageExcelData", order = 1)]
public class LanguageExcelData : ExcelDataBase<LanguageExcelItem>
{
}

#if UNITY_EDITOR
public class LanguageAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		LanguageExcelItem[] items = new LanguageExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			LanguageExcelItem item = new LanguageExcelItem();
			item.id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			item.ZH = allItemValueRowList[i]["ZH"];
			item.EN = allItemValueRowList[i]["EN"];
			items[i] = item;
		}
		LanguageExcelData excelDataAsset = ScriptableObject.CreateInstance<LanguageExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(LanguageExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif



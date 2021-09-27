/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using Common;

[Serializable]
public class ConstantExcelItem : ExcelItemBase
{
	public int value;
	public string desc;
}

[CreateAssetMenu(fileName = "ConstantExcelData", menuName = "Excel To ScriptableObject/Create ConstantExcelData", order = 1)]
public class ConstantExcelData : ExcelDataBase<ConstantExcelItem>
{
}

#if UNITY_EDITOR
public class ConstantAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		ConstantExcelItem[] items = new ConstantExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			ConstantExcelItem item = new ConstantExcelItem();
			item.id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			item.value = Convert.ToInt32(allItemValueRowList[i]["value"]);
			item.desc = allItemValueRowList[i]["desc"];
			items[i] = item;
		}
		ConstantExcelData excelDataAsset = ScriptableObject.CreateInstance<ConstantExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(ConstantExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif



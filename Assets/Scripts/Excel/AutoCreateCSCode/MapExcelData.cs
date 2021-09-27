/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using Common;

[Serializable]
public class MapExcelItem : ExcelItemBase
{
	public string Name;
	public RoomType Type;
	public int MonsterID;
	public int Pos0;
	public int Pos1;
	public int Pos2;
	public int Pos3;
	public int Pos4;
	public int Pos5;
	public int Pos6;
	public int Pos7;
	public int Pos8;
}

[CreateAssetMenu(fileName = "MapExcelData", menuName = "Excel To ScriptableObject/Create MapExcelData", order = 1)]
public class MapExcelData : ExcelDataBase<MapExcelItem>
{
}

#if UNITY_EDITOR
public class MapAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		MapExcelItem[] items = new MapExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			MapExcelItem item = new MapExcelItem();
			item.id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			item.Name = allItemValueRowList[i]["Name"];
			item.Type = (RoomType)Enum.Parse(typeof(RoomType),allItemValueRowList[i]["Type"]);
			item.MonsterID = Convert.ToInt32(allItemValueRowList[i]["MonsterID"]);
			item.Pos0 = Convert.ToInt32(allItemValueRowList[i]["Pos0"]);
			item.Pos1 = Convert.ToInt32(allItemValueRowList[i]["Pos1"]);
			item.Pos2 = Convert.ToInt32(allItemValueRowList[i]["Pos2"]);
			item.Pos3 = Convert.ToInt32(allItemValueRowList[i]["Pos3"]);
			item.Pos4 = Convert.ToInt32(allItemValueRowList[i]["Pos4"]);
			item.Pos5 = Convert.ToInt32(allItemValueRowList[i]["Pos5"]);
			item.Pos6 = Convert.ToInt32(allItemValueRowList[i]["Pos6"]);
			item.Pos7 = Convert.ToInt32(allItemValueRowList[i]["Pos7"]);
			item.Pos8 = Convert.ToInt32(allItemValueRowList[i]["Pos8"]);
			items[i] = item;
		}
		MapExcelData excelDataAsset = ScriptableObject.CreateInstance<MapExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(MapExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif



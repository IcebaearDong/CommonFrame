/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using Common;

[Serializable]
public class BuffExcelItem : ExcelItemBase
{
	public BuffType Type;
	public string Name;
	public BuffTgrType TriggerStage;
	public BuffEffectType Effect;
	public TargetMainType TargetMainType;
	public UnitType UnitType;
	public SelectType SelectType;
	public SelfPosType SelfPosType;
	public int Value;
	public int PerValue;
	public int Odds;
	public int FixedPosX;
	public int FixedPosY;
	public int Random;
}

[CreateAssetMenu(fileName = "BuffExcelData", menuName = "Excel To ScriptableObject/Create BuffExcelData", order = 1)]
public class BuffExcelData : ExcelDataBase<BuffExcelItem>
{
}

#if UNITY_EDITOR
public class BuffAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		BuffExcelItem[] items = new BuffExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			BuffExcelItem item = new BuffExcelItem();
			item.id = Convert.ToInt32(allItemValueRowList[i]["id"]);
			item.Type = (BuffType)Enum.Parse(typeof(BuffType),allItemValueRowList[i]["Type"]);
			item.Name = allItemValueRowList[i]["Name"];
			item.TriggerStage = (BuffTgrType)Enum.Parse(typeof(BuffTgrType),allItemValueRowList[i]["TriggerStage"]);
			item.Effect = (BuffEffectType)Enum.Parse(typeof(BuffEffectType),allItemValueRowList[i]["Effect"]);
			item.TargetMainType = (TargetMainType)Enum.Parse(typeof(TargetMainType),allItemValueRowList[i]["TargetMainType"]);
			item.UnitType = (UnitType)Enum.Parse(typeof(UnitType),allItemValueRowList[i]["UnitType"]);
			item.SelectType = (SelectType)Enum.Parse(typeof(SelectType),allItemValueRowList[i]["SelectType"]);
			item.SelfPosType = (SelfPosType)Enum.Parse(typeof(SelfPosType),allItemValueRowList[i]["SelfPosType"]);
			item.Value = Convert.ToInt32(allItemValueRowList[i]["Value"]);
			item.PerValue = Convert.ToInt32(allItemValueRowList[i]["PerValue"]);
			item.Odds = Convert.ToInt32(allItemValueRowList[i]["Odds"]);
			item.FixedPosX = Convert.ToInt32(allItemValueRowList[i]["FixedPosX"]);
			item.FixedPosY = Convert.ToInt32(allItemValueRowList[i]["FixedPosY"]);
			item.Random = Convert.ToInt32(allItemValueRowList[i]["Random"]);
			items[i] = item;
		}
		BuffExcelData excelDataAsset = ScriptableObject.CreateInstance<BuffExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(BuffExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif



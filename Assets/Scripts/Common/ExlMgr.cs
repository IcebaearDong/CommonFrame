/*Auto Create, Don't Edit !!!*/
using System.Collections.Generic;
using System;

public class ExlMgr : Singleton<ExlMgr>
{
	Dictionary<Type, object> excelDataDic = new Dictionary<Type, object>();
	private T GetExcelData<T, V>() where T : ExcelDataBase<V> where V : ExcelItemBase
	{
		Type type = typeof(T);
		if (excelDataDic.ContainsKey(type) && excelDataDic[type] is T)
			return excelDataDic[type] as T;
		T excelData = ResMgr.Inst.LoadResource<T>("ExcelAsset/" + type.Name);
		if (excelData != null)
			excelDataDic.Add(type, excelData as T);
		return excelData;
	}
	public BuffExcelItem GetBuffCfg(int id)
	{
		var data=GetExcelData<BuffExcelData, BuffExcelItem>();
		if (data != null)
			return data.GetExcelItem(id);
		return null;
	}

	public BuffExcelItem[] GetAllBuffCfg()
	{
		var data=GetExcelData<BuffExcelData, BuffExcelItem>();
		if (data != null)
			return data.GetExcelItems();
		return null;
	}

	public ConstantExcelItem GetConstantCfg(int id)
	{
		var data=GetExcelData<ConstantExcelData, ConstantExcelItem>();
		if (data != null)
			return data.GetExcelItem(id);
		return null;
	}

	public ConstantExcelItem[] GetAllConstantCfg()
	{
		var data=GetExcelData<ConstantExcelData, ConstantExcelItem>();
		if (data != null)
			return data.GetExcelItems();
		return null;
	}

	public HeroExcelItem GetHeroCfg(int id)
	{
		var data=GetExcelData<HeroExcelData, HeroExcelItem>();
		if (data != null)
			return data.GetExcelItem(id);
		return null;
	}

	public HeroExcelItem[] GetAllHeroCfg()
	{
		var data=GetExcelData<HeroExcelData, HeroExcelItem>();
		if (data != null)
			return data.GetExcelItems();
		return null;
	}

	public LanguageExcelItem GetLanguageCfg(int id)
	{
		var data=GetExcelData<LanguageExcelData, LanguageExcelItem>();
		if (data != null)
			return data.GetExcelItem(id);
		return null;
	}

	public LanguageExcelItem[] GetAllLanguageCfg()
	{
		var data=GetExcelData<LanguageExcelData, LanguageExcelItem>();
		if (data != null)
			return data.GetExcelItems();
		return null;
	}

	public MapExcelItem GetMapCfg(int id)
	{
		var data=GetExcelData<MapExcelData, MapExcelItem>();
		if (data != null)
			return data.GetExcelItem(id);
		return null;
	}

	public MapExcelItem[] GetAllMapCfg()
	{
		var data=GetExcelData<MapExcelData, MapExcelItem>();
		if (data != null)
			return data.GetExcelItems();
		return null;
	}

	public MonsterExcelItem GetMonsterCfg(int id)
	{
		var data=GetExcelData<MonsterExcelData, MonsterExcelItem>();
		if (data != null)
			return data.GetExcelItem(id);
		return null;
	}

	public MonsterExcelItem[] GetAllMonsterCfg()
	{
		var data=GetExcelData<MonsterExcelData, MonsterExcelItem>();
		if (data != null)
			return data.GetExcelItems();
		return null;
	}

}


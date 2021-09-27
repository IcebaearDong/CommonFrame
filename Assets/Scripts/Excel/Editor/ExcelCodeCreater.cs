#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Linq;
using System.IO;

public class ExcelCodeCreater
{

    #region --- Create Code ---

    //创建代码，生成数据C#类
    public static string CreateCodeStrByExcelData(ExcelMediumData excelMediumData)
    {
        if (excelMediumData == null)
            return null;
        //Excel名字
        string excelName = excelMediumData.excelName;
        if (string.IsNullOrEmpty(excelName))
            return null;
        //Dictionary<字段名称, 字段类型>
        Dictionary<string, string> propertyNameTypeDic = excelMediumData.propertyNameTypeDic;
        if (propertyNameTypeDic == null || propertyNameTypeDic.Count == 0)
            return null;
        //List<一行数据>，List<Dictionary<字段名称, 一行的每个单元格字段值>>
        List<Dictionary<string, string>> allItemValueRowList = excelMediumData.allItemValueRowList;
        if (allItemValueRowList == null || allItemValueRowList.Count == 0)
            return null;
        //行数据类名
        string itemClassName = excelName + "ExcelItem";
        //整体数据类名
        string dataClassName = excelName + "ExcelData";

        //生成类
        StringBuilder classSource = new StringBuilder();
        classSource.Append("/*Auto Create, Don't Edit !!!*/\n");
        classSource.Append("\n");
        //添加引用
        classSource.Append("using UnityEngine;\n");
        classSource.Append("using System.Collections.Generic;\n");
        classSource.Append("using System;\n");
        classSource.Append("using System.IO;\n");
        classSource.Append("using Common;\n");
        classSource.Append("\n");
        //生成行数据类，记录每行数据
        classSource.Append(CreateExcelRowItemClass(itemClassName, propertyNameTypeDic));
        classSource.Append("\n");
        //生成整体数据类，记录整个Excel的所有行数据
        classSource.Append(CreateExcelDataClass(dataClassName, itemClassName));
        classSource.Append("\n");
        //生成Asset操作类，用于自动创建Excel对应的Asset文件并赋值
        classSource.Append(CreateExcelAssetClass(excelMediumData));
        classSource.Append("\n");
        return classSource.ToString();
    }

    //----------

    //生成行数据类
    private static StringBuilder CreateExcelRowItemClass(string itemClassName, Dictionary<string, string> propertyNameTypeDic)
    {
        //生成Excel行数据类
        StringBuilder classSource = new StringBuilder();
        classSource.Append("[Serializable]\n");
        classSource.Append("public class " + itemClassName + " : ExcelItemBase\n");
        classSource.Append("{\n");
        //声明所有字段
        foreach (var item in propertyNameTypeDic)
        {
            classSource.Append(CreateCodeProperty(item.Key, item.Value));
        }
        classSource.Append("}\n");
        return classSource;
    }

    //声明行数据类字段
    private static string CreateCodeProperty(string name, string type)
    {
        if (string.IsNullOrEmpty(name))
            return null;
        if (name == "id")
            return null;

        bool isArray = type.Contains("[]");
        type = type.Replace("[]", string.Empty);
        //判断字段类型
        if (type == "int" || type == "Int" || type == "INT")
            type = "int";
        else if (type == "float" || type == "Float" || type == "FLOAT")
            type = "float";
        else if (type == "bool" || type == "Bool" || type == "BOOL")
            type = "bool";
        else if (type.StartsWith("enum") || type.StartsWith("Enum") || type.StartsWith("ENUM"))
            type = type.Split('|').LastOrDefault();
        else
            type = "string";

        type = isArray ? type + "[]" : type;
        //声明
        string propertyStr = "\tpublic " + type + " " + name + ";\n";
        return propertyStr;
    }

    //----------

    //生成数据类
    private static StringBuilder CreateExcelDataClass(string dataClassName, string itemClassName)
    {
        StringBuilder classSource = new StringBuilder();
        classSource.Append("[CreateAssetMenu(fileName = \"" + dataClassName + "\", menuName = \"Excel To ScriptableObject/Create " + dataClassName + "\", order = 1)]\n");
        classSource.Append("public class " + dataClassName + " : ExcelDataBase<" + itemClassName + ">\n");
        classSource.Append("{\n");
        //声明字段，行数据类数组
        //classSource.Append("\tpublic " + itemClassName + "[] items;\n");
        classSource.Append("}\n");
        return classSource;
    }

    //----------

    //生成Asset操作类
    private static StringBuilder CreateExcelAssetClass(ExcelMediumData excelMediumData)
    {
        if (excelMediumData == null)
            return null;

        string excelName = excelMediumData.excelName;
        if (string.IsNullOrEmpty(excelName))
            return null;

        Dictionary<string, string> propertyNameTypeDic = excelMediumData.propertyNameTypeDic;
        if (propertyNameTypeDic == null || propertyNameTypeDic.Count == 0)
            return null;

        List<Dictionary<string, string>> allItemValueRowList = excelMediumData.allItemValueRowList;
        if (allItemValueRowList == null || allItemValueRowList.Count == 0)
            return null;

        string itemClassName = excelName + "ExcelItem";
        string dataClassName = excelName + "ExcelData";

        StringBuilder classSource = new StringBuilder();
        classSource.Append("#if UNITY_EDITOR\n");
        //类名
        classSource.Append("public class " + excelName + "AssetAssignment\n");
        classSource.Append("{\n");
        //方法名
        classSource.Append("\tpublic static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)\n");
        //方法体，若有需要可加入try/catch
        classSource.Append("\t{\n");
        classSource.Append("\t\tif (allItemValueRowList == null || allItemValueRowList.Count == 0)\n");
        classSource.Append("\t\t\treturn false;\n");
        classSource.Append("\t\tint rowCount = allItemValueRowList.Count;\n");
        classSource.Append("\t\t" + itemClassName + "[] items = new " + itemClassName + "[rowCount];\n");
        classSource.Append("\t\tfor (int i = 0; i < items.Length; i++)\n");
        classSource.Append("\t\t{\n");
        classSource.Append($"\t\t\t{itemClassName} item = new {itemClassName}();\n");
        foreach (var item in propertyNameTypeDic)
        {
            bool isArray = item.Value.Contains("[]");
            string str = string.Empty;
            int idx = 0;
            if (isArray)
            {
                idx++;
                classSource.Append($"\t\t\tstring[] {item.Key}Arr  = allItemValueRowList[i][\"{ item.Key }\"].Split(',');\n");
                classSource.Append($"\t\t\titem.{item.Key} = new {item.Value.Replace("[]", string.Empty)}[{item.Key}Arr.Length];\n ");
                classSource.Append($"\t\t\tfor (int i{idx} = 0; i{idx} < {item.Key}Arr.Length; i{idx}++)\n");
                classSource.Append("\t\t\t{\n");
                str = AssignmentCodeProperty($"{item.Key}Arr[i{idx}]", item.Value);
                classSource.Append($"\t\t\t\t item.{item.Key}[i{idx}]={str}");
                classSource.Append(";\n");
                classSource.Append("\t\t\t}\n");
            }
            else
            {
                classSource.Append("\t\t\titem." + item.Key + " = ");
                str = "allItemValueRowList[i][\"" + item.Key + "\"]";
                classSource.Append(AssignmentCodeProperty(str, item.Value));
                classSource.Append(";\n");
            }
        }

        classSource.Append($"\t\t\titems[i] = item;\n");
        classSource.Append("\t\t}\n");
        classSource.Append("\t\t" + dataClassName + " excelDataAsset = ScriptableObject.CreateInstance<" + dataClassName + ">();\n");
        classSource.Append("\t\texcelDataAsset.items = items;\n");
        classSource.Append("\t\tif (!Directory.Exists(excelAssetPath))\n");
        classSource.Append("\t\t\tDirectory.CreateDirectory(excelAssetPath);\n");
        classSource.Append("\t\tstring pullPath = excelAssetPath + \"/\" + typeof(" + dataClassName + ").Name + \".asset\";\n");
        classSource.Append("\t\tUnityEditor.AssetDatabase.DeleteAsset(pullPath);\n");
        classSource.Append("\t\tUnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);\n");
        classSource.Append("\t\tUnityEditor.AssetDatabase.Refresh();\n");
        classSource.Append("\t\treturn true;\n");
        classSource.Append("\t}\n");
        //
        classSource.Append("}\n");
        classSource.Append("#endif\n");
        return classSource;
    }

    //声明Asset操作类字段
    private static string AssignmentCodeProperty(string stringValue, string type)
    {
        //判断类型
        if (type == "int" || type == "Int" || type == "INT" || type == "int[]")
        {
            return "Convert.ToInt32(" + stringValue + ")";
        }
        else if (type == "float" || type == "Float" || type == "FLOAT")
        {
            return "Convert.ToSingle(" + stringValue + ")";
        }
        else if (type == "bool" || type == "Bool" || type == "BOOL")
        {
            return "Convert.ToBoolean(" + stringValue + ")";
        }
        else if (type.StartsWith("enum") || type.StartsWith("Enum") || type.StartsWith("ENUM"))
        {
            string enumName = type.Split('|').LastOrDefault();
            return $"({enumName})Enum.Parse(typeof({enumName}),{stringValue})";
        }
        else
            return stringValue;
    }

    // 生成ExlMgr代码
    public static string CreateExcelMgrCode()
    {
        StringBuilder code = new StringBuilder();
        code.Append("/*Auto Create, Don't Edit !!!*/\n");
        code.Append("using System.Collections.Generic;\n");
        code.Append("using System;\n");
        code.Append("\n");

        code.Append("public class ExlMgr : Singleton<ExlMgr>\n");
        code.Append("{\n");
        code.Append("\tDictionary<Type, object> excelDataDic = new Dictionary<Type, object>();\n");
        code.Append("\tprivate T GetExcelData<T, V>() where T : ExcelDataBase<V> where V : ExcelItemBase\n");
        code.Append("\t{\n");
        code.Append("\t\tType type = typeof(T);\n");
        code.Append("\t\tif (excelDataDic.ContainsKey(type) && excelDataDic[type] is T)\n");
        code.Append("\t\t\treturn excelDataDic[type] as T;\n");
        code.Append("\t\tT excelData = ResMgr.Inst.LoadResource<T>(\"ExcelAsset/\" + type.Name);\n");
        code.Append("\t\tif (excelData != null)\n");
        code.Append("\t\t\texcelDataDic.Add(type, excelData as T);\n");
        code.Append("\t\treturn excelData;\n");
        code.Append("\t}\n");

        List<string> list = Directory.GetFiles(MyExcelDataReader.excelFilePath).ToList();
        list = list.FindAll(t => !t.Contains("meta"));

        foreach (var item in list)
        {
            FileInfo fileInfo = new FileInfo(item);
            string name = $"{fileInfo.Name.Replace(".xlsx", string.Empty)}";
            code.Append(GetExcelItem(name));
        }

        code.Append("}\n");
        return code.ToString();
    }

    private static string GetExcelItem(string name)
    {
        string dataName = $"{name}ExcelData";
        string itemName = $"{name}ExcelItem";

        StringBuilder sb = new StringBuilder();
        sb.Append($"\tpublic {itemName} Get{name}Cfg(int id)\n");
        sb.Append("\t{\n");
        sb.Append($"\t\tvar data=GetExcelData<{dataName}, {itemName}>();\n");
        sb.Append("\t\tif (data != null)\n");
        sb.Append("\t\t\treturn data.GetExcelItem(id);\n");
        sb.Append("\t\treturn null;\n");
        sb.Append("\t}\n");
        sb.Append("\n");
        sb.Append($"\tpublic {name}ExcelItem[] GetAll{name}Cfg()\n");
        sb.Append("\t{\n");
        sb.Append($"\t\tvar data=GetExcelData<{dataName}, {itemName}>();\n");
        sb.Append("\t\tif (data != null)\n");
        sb.Append("\t\t\treturn data.GetExcelItems();\n");
        sb.Append("\t\treturn null;\n");
        sb.Append("\t}\n");
        sb.Append("\n");

        return sb.ToString();
    }
    #endregion

}
#endif
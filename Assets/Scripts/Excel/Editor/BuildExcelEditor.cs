#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;
using System.Reflection;
using System;

public class BuildExcelEditor : Editor
{

}

public class BuildExcelWindow : EditorWindow
{
    //[MenuItem("MyTools/Excel/Build Script")]
    //public static void CreateExcelCode()
    //{
    //    ExcelDataReader.ReadAllExcelToCode();
    //}

    //[MenuItem("MyTools/Excel/Build Asset")]
    //public static void CreateExcelAssset()
    //{
    //    ExcelDataReader.CreateAllExcelAsset();
    //}

    [MenuItem("MyTools/Excel Window")]
    public static void ShowExcelWindow()
    {
        //��ʾ�������ڷ�ʽһ
        //BuildExcelWindow buildExcelWindow = GetWindow<BuildExcelWindow>();
        //buildExcelWindow.Show();
        //��ʾ�������ڷ�ʽ��
        EditorWindow.GetWindow(typeof(BuildExcelWindow));
    }

    private string showNotify;
    private Vector2 scrollPosition = Vector2.zero;

    private List<string> fileNameList = new List<string>();
    private List<string> filePathList = new List<string>();

    private void Awake()
    {
        titleContent.text = "Excel���ݶ�ȡ";
    }

    private void OnEnable()
    {
        showNotify = "";
        GetExcelFile();
    }

    private void OnDisable()
    {
        showNotify = "";

        fileNameList.Clear();
        filePathList.Clear();
    }

    private void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition,
            GUILayout.Width(position.width), GUILayout.Height(position.height));
        //�Զ�����C#�ű�
        GUILayout.Space(10);
        GUILayout.Label("Excel To Script And Asset");
        for (int i = 0; i < fileNameList.Count; i++)
        {
            if (GUILayout.Button(fileNameList[i], GUILayout.Width(200), GUILayout.Height(30)))
            {
                SelectExcelToCodeByIndex(i);
                SelectCodeToAssetByIndex(i);
            }
        }
        if (GUILayout.Button("All Excel", GUILayout.Width(200), GUILayout.Height(30)))
        {
            SelectExcelToCodeByIndex(-1);
            SelectCodeToAssetByIndex(-1);
        }
        //
        GUILayout.Space(20);
        GUILayout.Label(showNotify);

        if (GUILayout.Button("SaveChangeToExcel", GUILayout.Width(200), GUILayout.Height(30)))
        {
            List<string> list = Directory.GetFiles(MyExcelDataReader.excelFilePath).ToList();
            list = list.FindAll(t => !t.Contains("meta"));
            foreach (var item in list)
            {
                FileInfo fileInfo = new FileInfo(item);
                string name = $"{fileInfo.Name.Replace(".xlsx", string.Empty)}ExcelData";
                dynamic d = Resources.Load($"ExcelAsset/{name}");
                using (ExcelPackage pkg = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet sheet = pkg.Workbook.Worksheets[1];
                    int dataRow = MyExcelDataReader.excelDataRow;
                    int colum = sheet.Dimension.Columns;
                    dynamic fData = d.items[0];
                    FieldInfo[] info = fData.GetType().GetFields();
                    FieldInfo id = info[info.Length - 1]; // ��Ϊid�ֶ������
                    for (int i = info.Length - 1; i >= 1; i--)
                    {
                        info[i] = info[i - 1];
                    }

                    info[0] = id;

                    foreach (var data in d.items)
                    {
                        int curID = info[0].GetValue(data);
                        for (int i = 0; i < colum; i++)
                        {
                            ExcelRange excelRange = sheet.Cells[dataRow, i + 1];
                            object oD = info[i].GetValue(data);
                            if (excelRange.Value != null) // �Ķ�
                            {
                                object oE = excelRange.Value;

                                if (oE.ToString() != oD.ToString())
                                    Debug.Log($"{sheet.Name}��ID={Helper.Color("yellow", curID)}�ֶ�:{info[i].Name}[{dataRow},{i + 1}]��<color=yellow>{oE}</color>=><color=green>{oD}</color>");
                            }
                            else  // ����
                            {
                                Debug.Log($"{sheet.Name}��ID={Helper.Color("yellow", curID)}�¼�");
                            }

                            excelRange.Value = oD;
                        }

                        dataRow++;
                    }

                    pkg.Save();
                }
            }
        }

        if (GUILayout.Button("CreateExcMgrCode", GUILayout.Width(200), GUILayout.Height(30)))
        {
            MyExcelDataReader.CreateExlMgrCode();
        }
        //
        GUILayout.EndScrollView();
        //this.Repaint();
    }

    //��ȡָ��·���µ�Excel�ļ���
    private void GetExcelFile()
    {
        fileNameList.Clear();
        filePathList.Clear();

        if (!Directory.Exists(MyExcelDataReader.excelFilePath))
        {
            showNotify = "��Ч·����" + MyExcelDataReader.excelFilePath;
            return;
        }
        string[] excelFileFullPaths = Directory.GetFiles(MyExcelDataReader.excelFilePath, "*.xlsx");

        if (excelFileFullPaths == null || excelFileFullPaths.Length == 0)
        {
            showNotify = MyExcelDataReader.excelFilePath + "·����û���ҵ�Excel�ļ�";
            return;
        }

        filePathList.AddRange(excelFileFullPaths);
        for (int i = 0; i < filePathList.Count; i++)
        {
            string fileName = filePathList[i].Split('/').LastOrDefault();
            fileName = filePathList[i].Split('\\').LastOrDefault();
            fileNameList.Add(fileName);
        }
        showNotify = "�ҵ�Excel�ļ���" + fileNameList.Count + "��";
    }

    //�Զ�����C#�ű�
    private void SelectExcelToCodeByIndex(int index)
    {
        if (index >= 0 && index < filePathList.Count)
        {
            string fullPath = filePathList[index];
            MyExcelDataReader.ReadOneExcelToCode(fullPath);
        }
        else
        {
            MyExcelDataReader.ReadAllExcelToCode();
        }
    }

    //�Զ�����Asset�ļ�
    private void SelectCodeToAssetByIndex(int index)
    {
        if (index >= 0 && index < filePathList.Count)
        {
            string fullPath = filePathList[index];
            MyExcelDataReader.CreateOneExcelAsset(fullPath);
        }
        else
        {
            MyExcelDataReader.CreateAllExcelAsset();
        }
    }

    public static class Helper
    {
        public static string Color(string color, object o)
        {
            return $"<color={color}>{o}</color>";
        }
    }

}

#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class StatusManager : Singleton<StatusManager>
{
    private GameManager gameManager;

    public bool debug;

    private const string SAVELOADSTR = "DATAS";

    public Student[] studentdata;//scriptableObject
    public ClassRoom[] classroom;//scriptableObject
    public Stock[] stock;//�ֽ�
    public StudentPresident president;//�л�ȸ��
    public const int STOCK_Y_POS_COUNT = 5;//�ֽ� Y��ǥ ����

    [HideInInspector]
    public StatusSave statDatas = new StatusSave();

    private void Awake()
    {
        if (debug == true)
        {
            ReMovePrefsKey();
        }
        gameManager = GameManager.Instance;

        //SetDataToJson();
        GetDataToJson();
    }
    private void OnEnable()
    {
        print(statDatas.effort);
    }
    //Application.persistentDataPath;

    /// <summary>
    /// PlayerPrefs�� ����� �����͸� �����ϴ� �Լ�
    /// </summary>
    private void ReMovePrefsKey()
    {
        //just for debug
        PlayerPrefs.DeleteKey(SAVELOADSTR);
    }
    /// <summary>
    /// �����ߴ� �����͸� �ҷ����� �Լ�
    /// </summary>
    public void LoadData()
    {
        gameManager.Effort = statDatas.effort;
        president.Level = statDatas.studentPresidentLevel;
        for (int i = 0; i < studentdata.Length; i++)
        {
            print(i);
            studentdata[i].studentData.level = statDatas.studentLevel[i];
        }
        for (int i = 0; i < classroom.Length; i++)
        {
            classroom[i].IsBought = statDatas.classBoolean[i];
            classroom[i].classData.currentCost = statDatas.classCurCost[i];
        }
        for (int i = 0; i < stock.Length; i++)
        {
            for (int j = 0; j < STOCK_Y_POS_COUNT; j++)
            {
                stock[i].posY[j] = statDatas.dotYPos[i, j];
            }
            stock[i].quitTime = statDatas.quitTime[i];
            stock[i].CycleDelay = statDatas.cycleTime[i];
            stock[i].Have = statDatas.stockHave[i];
        }
    }
    /// <summary>
    /// �����͸� �����ϴ� �Լ�
    /// </summary>
    public void SaveData()
    {
        statDatas.effort = gameManager.Effort;
        statDatas.studentPresidentLevel = president.Level;
        for (int i = 0; i < studentdata.Length; i++)
        {
            print(studentdata.Length);
            statDatas.studentLevel[i] = studentdata[i].studentData.level;
        }
        for (int i = 0; i < classroom.Length; i++)
        {
            statDatas.classBoolean[i] = classroom[i].IsBought;
            statDatas.classCurCost[i] = classroom[i].classData.currentCost;
        }
        for (int i = 0; i < stock.Length; i++)
        {
            for (int j = 0; j < STOCK_Y_POS_COUNT; j++)
            {
                statDatas.dotYPos[i, j] = stock[i].posY[j];
            }
            statDatas.quitTime[i] = stock[i].quitTime;
            statDatas.cycleTime[i] = stock[i].CycleDelay;
            statDatas.stockHave[i] = stock[i].Have;
        }
    }
    public void SetDataToJson()
    {
        SaveData();
        string str = JsonUtility.ToJson(statDatas, true);
        PlayerPrefs.SetString(SAVELOADSTR, str);
#if UNITY_EDITOR
        //Application.persistentDataPath;
        //File.WriteAllText($"{Application.dataPath}/Json.txt", str);
        Debug.Log(str);
#else
        File.WriteAllText($"{Application.persistentDataPath}/MobileJson.txt", str);
#endif
    }
    public void GetDataToJson()
    {
        string path;
#if UNITY_EDITOR
        path = $"{Application.dataPath}/Json.txt";
#else
        path = $"{Application.persistentDataPath}/MobileJson.txt";
#endif

        string info = PlayerPrefs.GetString(SAVELOADSTR, "none");
        Debug.Log(info);
        //if (File.Exists(path) == false) return;
        //string str = File.ReadAllText(path);
        if (info == "none")
        {
            Debug.Log("None");
            return;
        }
        StatusSave temp = JsonUtility.FromJson<StatusSave>(info);
        if (temp != null)
        {
            statDatas = temp;
            LoadData();
            Debug.Log("�ҷ���");
        }


    }
    private void OnApplicationQuit()
    {
        SetDataToJson();
    }
    private void OnApplicationPause(bool pause)
    {
        //SetDataToJson();
    }
}
[Serializable]
public class StatusSave
{
    public ulong effort;//���� ������ �ִ� ��
    public int studentPresidentLevel;//�л�ȸ�� ����
    public int[] studentLevel = new int[5];//�л� ����
    public bool[] classBoolean = new bool[5];//������ ���
    public ulong[] classCurCost = new ulong[5];//���� ���� ����
    public float[,] dotYPos = new float[5, 5];//�ֽ� Y��ǥ
    public float[] quitTime = new float[5];//�����ð�
    public float[] cycleTime = new float[5];//�ֽ� �����ð�
    public int[] stockHave = new int[5];//�ֽ� �������ִ� ����
}

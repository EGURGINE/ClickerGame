using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class StatusManager : Singleton<StatusManager>
{
    private GameManager gameManager;

    private const string SAVELOADSTR = "DATAS";

    public Student[] studentdata;//scriptableObject
    public ClassRoom[] classroom;//scriptableObject
    public Stock[] stock;//주식
    public StudentPresident president;//학생회장
    public const int STOCK_Y_POS_COUNT = 5;//주식 Y좌표 개수

    [HideInInspector]
    public StatusSave statDatas = new StatusSave();

    private void Awake()
    {
        gameManager = GameManager.Instance;

        ReMovePrefsKey();

        //SetDataToJson();
        GetDataToJson();
    }
    private void OnEnable()
    {
        print(statDatas.effort);
    }
    private void Start()
    {

    }
    //Application.persistentDataPath;
    private void ReMovePrefsKey()
    {
        PlayerPrefs.DeleteKey(SAVELOADSTR);
    }
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
            return;
        }
        StatusSave temp = JsonUtility.FromJson<StatusSave>(info);
        if (temp != null)
        {
            statDatas = temp;
            LoadData();
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
    public ulong effort;//현재 가지고 있는 돈
    public int studentPresidentLevel;//학생회장 레벨
    public int[] studentLevel = new int[5];//학생 레벨
    public bool[] classBoolean = new bool[5];//교실을 삿냐
    public ulong[] classCurCost = new ulong[5];//현재 교실 가격
    public float[,] dotYPos = new float[5, 5];//주식 Y좌표
    public float[] quitTime = new float[5];//나간시간
    public float[] cycleTime = new float[5];//주식 리젠시간
    public int[] stockHave = new int[5];//주식 가지고있는 갯수
}

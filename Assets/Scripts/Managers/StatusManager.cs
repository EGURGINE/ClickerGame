using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class StatusManager : Singleton<StatusManager>
{
    private GameManager gameManager;
    private const string SAVESTR = "SaveData";
    private const string LOADSTR = "LoadData";

    public Student[] studentdata;//scriptableObject
    public ClassRoom[] classroom;//scriptableObject
    public Stock[] stock;//주식
    public StudentPresident president;//학생회장
    public const int STOCKYPOSCOUNT = 5;//주식 Y좌표 개수


    [HideInInspector]
    public StatusSave statData = new StatusSave();

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    private void Start()
    {
        president = GetComponent<StudentPresident>();

    }
    public void LoadData()
    {
        gameManager.Effort = statData.effort;
        president.Level = statData.studentPresidentLevel;
        for (int i = 0; i < studentdata.Length; i++)
        {
            studentdata[i].studentData.level = statData.studentLevel[i];
        }
        for (int i = 0; i < classroom.Length; i++)
        {
            classroom[i].IsBought = statData.classBoolean[i];
            classroom[i].classData.currentCost = statData.classCurCost[i];
        }
        for (int i = 0; i < stock.Length; i++)
        {
            for (int j = 0; j < STOCKYPOSCOUNT; j++)
            {
                stock[i].posY[j] = statData.dotYPos[i, j];
            }
            stock[i].quitTime = statData.quitTime[i];
            stock[i].CycleDelay = statData.cycleTime[i];
            stock[i].Have = statData.stockHave[i];
        }
    }
    public void SaveData()
    {
        statData.effort = gameManager.Effort;
        statData.studentPresidentLevel = president.Level;
        for (int i = 0; i < studentdata.Length; i++)
        {
            statData.studentLevel[i] = studentdata[i].studentData.level;
        }
        for (int i = 0; i < classroom.Length; i++)
        {
            statData.classBoolean[i] = classroom[i].IsBought;
            statData.classCurCost[i] = classroom[i].classData.currentCost;
        }
        for (int i = 0; i < stock.Length; i++)
        {
            for (int j = 0; j < STOCKYPOSCOUNT; j++)
            {
                statData.dotYPos[i, j] = stock[i].posY[j];
            }
            statData.quitTime[i] = stock[i].quitTime;
            statData.cycleTime[i] = stock[i].CycleDelay;
            statData.stockHave[i] = stock[i].Have;
        }
    }
    public void SetDataToJson()
    {
        SaveData();
        string str = JsonUtility.ToJson(statData);
        PlayerPrefs.SetString("Datas", str);
#if UNITY_EDITOR
        //Application.persistentDataPath;
        File.WriteAllText($"{Application.dataPath}/Json.txt", str);
#else
        File.WriteAllText($"{Application.persistentDataPath}/MobileJson.txt", str);
#endif

    }
        //Application.persistentDataPath;
    public void GetDataToJson()
    {
        LoadData();
#if !UNITY_EDITOR
        string str = File.ReadAllText($"{Application.dataPath}/Json.txt");
        JsonUtility.FromJson<StatusSave>(str);
        
#else
        string str = File.ReadAllText($"{Application.persistentDataPath}/MobileJson.txt");
        JsonUtility.FromJson<StatusSave>(str);

#endif

    }
}
[Serializable]
public class StatusSave
{
    public ulong effort;//현재 가지고 있는 돈
    public int studentPresidentLevel;//학생회장 레벨
    public int[] studentLevel;//학생 레벨
    public bool[] classBoolean;//교실을 삿냐
    public ulong[] classCurCost;//현재 교실 가격
    public float[,] dotYPos;//주식 Y좌표
    public float[] quitTime;//나간시간
    public float[] cycleTime;//주식 리젠시간
    public int[] stockHave;//주식 가지고있는 갯수
}

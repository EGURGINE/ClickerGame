using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatusManager : Singleton<StatusManager>
{
    GameManager gameManager;
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
                statData.dotYPos[i,j] = stock[i].posY[j];
            }
            statData.quitTime[i] = stock[i].quitTime;
            statData.cycleTime[i] = stock[i].CycleDelay;
            statData.stockHave[i] = stock[i].Have;
        }
        
    }
    public void SetDataToJson()
    {
        string str = JsonUtility.ToJson(statData,true);
        PlayerPrefs.SetString(SAVESTR, str);
    }
    public void GetDataToJson()
    {

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatusManager : Singleton<StatusManager>
{
    public StudentData[] studentdata;//scriptableObject
    public ClassRoom[] classroom;//scriptableObject

    [HideInInspector]
    public StatusSave statData = new StatusSave();

    [Tooltip("현재 가지고 있는 돈")]
    public ulong effort;//현재 가지고 있는 돈

    [Header("학생")]
    public int studentPresidentLevel;//학생회장 레벨
    public int[] studentLevel;//학생 레벨

    [Header("교실")]
    public bool[] classBoolean;//교실을 삿냐
    public ulong[] classCurCost;//교실 가격

    [Header("주식")]
    public float[] dotYPos;//주식점의 Y좌표
    public float cycleTime;//주식 리젠시간
    public int stockHave;//주식 가지고있는 갯수

    [Tooltip("나간시간")]
    public float quitTime;//나간시간
    public void GetData()
    {
        effort = statData.effort;
        studentPresidentLevel = statData.studentPresidentLevel;
        studentLevel = statData.studentLevel;
        classBoolean = statData.classBoolean;
        classCurCost = statData.classCurCost;
        dotYPos = statData.dotYPos;
        cycleTime = statData.cycleTime;
        stockHave = statData.stockHave;
    }
    public void SetDataToSave()
    {
        statData.effort = effort;
        statData.studentPresidentLevel = studentPresidentLevel;
        statData.studentLevel = studentLevel;
        statData.classBoolean = classBoolean;
        statData.classCurCost = classCurCost;
        statData.dotYPos = dotYPos;
        statData.quitTime = quitTime;
        statData.cycleTime = cycleTime;
        statData.stockHave = stockHave;
    }
    public void SaveData()
    {
        string saveStr = JsonUtility.ToJson(statData, true);
        PlayerPrefs.SetString(saveStr, saveStr);
        print(saveStr);
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
    public float[] dotYPos;//주식점의 Y좌표
    public float quitTime;//나간시간
    public float cycleTime;//주식 리젠시간
    public int stockHave;//주식 가지고있는 갯수
}

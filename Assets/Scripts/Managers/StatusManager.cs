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

    [Tooltip("���� ������ �ִ� ��")]
    public ulong effort;//���� ������ �ִ� ��

    [Header("�л�")]
    public int studentPresidentLevel;//�л�ȸ�� ����
    public int[] studentLevel;//�л� ����

    [Header("����")]
    public bool[] classBoolean;//������ ���
    public ulong[] classCurCost;//���� ����

    [Header("�ֽ�")]
    public float[] dotYPos;//�ֽ����� Y��ǥ
    public float cycleTime;//�ֽ� �����ð�
    public int stockHave;//�ֽ� �������ִ� ����

    [Tooltip("�����ð�")]
    public float quitTime;//�����ð�
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
    public ulong effort;//���� ������ �ִ� ��
    public int studentPresidentLevel;//�л�ȸ�� ����
    public int[] studentLevel;//�л� ����
    public bool[] classBoolean;//������ ���
    public ulong[] classCurCost;//���� ���� ����
    public float[] dotYPos;//�ֽ����� Y��ǥ
    public float quitTime;//�����ð�
    public float cycleTime;//�ֽ� �����ð�
    public int stockHave;//�ֽ� �������ִ� ����
}

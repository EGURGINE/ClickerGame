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
    public Stock[] stock;//�ֽ�
    public StudentPresident president;//�л�ȸ��
    public const int STOCKYPOSCOUNT = 5;//�ֽ� Y��ǥ ����


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
    public ulong effort;//���� ������ �ִ� ��
    public int studentPresidentLevel;//�л�ȸ�� ����
    public int[] studentLevel;//�л� ����
    public bool[] classBoolean;//������ ���
    public ulong[] classCurCost;//���� ���� ����
    public float[,] dotYPos;//�ֽ� Y��ǥ
    public float[] quitTime;//�����ð�
    public float[] cycleTime;//�ֽ� �����ð�
    public int[] stockHave;//�ֽ� �������ִ� ����
}

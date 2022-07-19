using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EStudentType
{
    MainProgramming,
    SubProGramming,
    TwoDGraphic,
    ThreeDGraphic,
    Designer
}
[CreateAssetMenu(fileName = "Student Data", menuName = "Scriptable Object/Student Data" , order = int.MaxValue)]
public class StudentData : ScriptableObject
{
    public Animator animator;
    public EStudentType studentType;
    public ulong timePerSecondProduct;//�ʴ� ���귮
    public ulong baseCost;//�⺻ ���� ���
    public ulong cost;//���� ���
    public float increment;//���۴� ��� ������
    public int level = 1;//����
    public string studentName;//�̸�
}

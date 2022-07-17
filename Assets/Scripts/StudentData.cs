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
    public float timePerSecondProduct;//�ʴ� ���귮
    public float baseCost;//�⺻ ���� ���
    public float increment;//���۴� ��� ������
    public float cost;//���� ���
    public int level;//����
    public string studentName;//�̸�
}

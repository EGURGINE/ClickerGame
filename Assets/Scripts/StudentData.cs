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
    public GameObject student;//3D object
    public Animator animator;//Animator
    public EStudentType studentType;//Type
    public ulong timePerSecondProduct;//�ʴ� ���귮
    public float productIncrement;//���� ������
    public ulong baseCost;//�⺻ ���� ���
    public ulong cost;//���� ���
    public float increment;//���۴� ��� ������
    public int level;//����
    public string studentName;//�̸�
}

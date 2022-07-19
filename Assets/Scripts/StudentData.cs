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
    public ulong timePerSecondProduct;//초당 생산량
    public ulong baseCost;//기본 업글 비용
    public ulong cost;//업글 비용
    public float increment;//업글당 비용 증가량
    public int level = 1;//레벨
    public string studentName;//이름
}

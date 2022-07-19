using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EClassType
{
    Kineung,
    Class301,
    Mac,
    Class305,
    Kiup
}
[CreateAssetMenu(fileName = "Class Data", menuName = "Scriptable Object/Class Data", order = int.MaxValue)]
public class ClassData : ScriptableObject
{
    public EClassType classType;
    public ulong timePerSecondProduct;//초당 가격증가량
    public ulong buyCost;//구매 비용
    public ulong currentCost;//현재 값어치
    public string className;
}
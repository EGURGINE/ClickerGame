using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EClassType
{
    Class301,
    Class305,
    Mac,
    Kineung,
    Kiup
}
[CreateAssetMenu(fileName = "Class Data", menuName = "Scriptable Object/Class Data",order = int.MaxValue)]
public class ClassData : ScriptableObject
{
    public EClassType classType;
    public float timePerSecondProduct;//초당 생산량
    public float baseCost;//기본 업글 비용
    public float increment;//증가량
    public float cost;//업글 비용
}


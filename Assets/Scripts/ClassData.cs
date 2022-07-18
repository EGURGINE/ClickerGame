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
[CreateAssetMenu(fileName = "Class Data", menuName = "Scriptable Object/Class Data",order = int.MaxValue)]
public class ClassData : ScriptableObject
{
    public EClassType classType;
    public float timePerSecondProduct;//�ʴ� ���귮
    public float cost;//���� ���
}


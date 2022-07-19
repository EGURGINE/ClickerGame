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
    public ulong timePerSecondProduct;//�ʴ� ����������
    public ulong buyCost;//���� ���
    public ulong currentCost;//���� ����ġ
    public string className;
}
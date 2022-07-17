using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClassRoom : MonoBehaviour
{
    public ClassData classData;


    [SerializeField]
    private TextMeshProUGUI costtxt;
    [SerializeField]
    
    public EClassType classType;
    public float timePerSecondProduct;//�ʴ� ���귮
    public float baseCost;//�⺻ ���� ���
    public float increment;//������
    
}

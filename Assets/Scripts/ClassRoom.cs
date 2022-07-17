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
    public float timePerSecondProduct;//초당 생산량
    public float baseCost;//기본 업글 비용
    public float increment;//증가량
    
}

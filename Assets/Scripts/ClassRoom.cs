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
    private TextMeshProUGUI className;

    public float cost;
    private float perSecondIncrement;//초당 실력 증가량
    public float PerSecondIncrement
    {
        get
        {
            return perSecondIncrement;
        }
        set
        {
            perSecondIncrement = value;
        }
    }
    
}

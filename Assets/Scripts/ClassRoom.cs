using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClassRoom : MonoBehaviour
{
    public ClassData classData;

    private bool isBought;
    public bool IsBought
    {
        get => isBought;
        set
        {
            isBought = value;
            if(isBought == true)
            {
                isBoughtImg.gameObject.SetActive(true);
            }
            else
            {
                isBoughtImg.gameObject.SetActive(false);
            }
        }
    }

    [SerializeField]
    private TextMeshProUGUI costtxt;
    [SerializeField]
    private TextMeshProUGUI className;
    [SerializeField]
    private Image isBoughtImg;

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

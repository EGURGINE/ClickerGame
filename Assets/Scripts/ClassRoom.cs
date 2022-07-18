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
                InvokeRepeating(nameof(TimePerProducting), 1f, 1f);
            }
            else
            {
                classData.currentCost = classData.buyCost;
            }
        }
    }
    private void TimePerProducting()
    {
        classData.currentCost += classData.timePerSecondProduct;
    }
    [Header("TextMeshPro")]
    [SerializeField]
    private TextMeshProUGUI costtxt;
    [SerializeField]
    private TextMeshProUGUI baseCost;
    [SerializeField]
    private TextMeshProUGUI className;

    private void Start()
    {
        className.text = $"{classData.className}";
    }
    private void Update()
    {
        costtxt.text = $"{classData.currentCost}";
    }



}

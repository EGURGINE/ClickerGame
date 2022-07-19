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
                InvokeRepeating(nameof(TimePerProducting), 1f, 2f);
                buyBar.SetActive(false);
                sellBar.SetActive(true);
            }
            else
            {
                CancelInvoke(nameof(TimePerProducting));
                classData.currentCost = classData.buyCost;
                sellBar.SetActive(false);
                buyBar.SetActive(true);
            }
        }
    }
    private void TimePerProducting()
    {
        classData.currentCost += classData.timePerSecondProduct;
    }
    [Header("TextMeshPro")]
    [SerializeField]
    private TextMeshProUGUI sellCosttxt;
    [SerializeField]
    private TextMeshProUGUI buyCosttxt;
    [SerializeField]
    private TextMeshProUGUI className;


    [Header("Buttons")]
    [Space(10f)]
    [SerializeField]
    private Button buyBtn;//��� ��ư
    [SerializeField]
    private Button sellBtn;//�Ĵ� ��ư

    [Header("Bars")]
    [SerializeField]
    private GameObject buyBar;//��� ��ư�� �ִ� Bar
    [SerializeField]
    private GameObject sellBar;//�Ĵ� ��ư�� �ִ� Bar
    

    private void Start()
    {
        className.text = $"{classData.className}";
        buyCosttxt.text = $"{classData.buyCost}";

        buyBtn.onClick.AddListener(() =>
        {
            IsBought = true;
        });
        sellBtn.onClick.AddListener(() =>
        {
            IsBought = false;
        });
    }
    private void Update()
    {
        sellCosttxt.text = $"{classData.currentCost}";
    }
}

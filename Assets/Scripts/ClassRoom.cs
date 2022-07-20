using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class ClassRoom : MonoBehaviour
{
    public ClassData classData;

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("IsBought" + classData.classType) == 1)
        {
            IsBought = true;
        }
        else
        {
            IsBought = false;
        }
    }
    private bool isBought;
    public bool IsBought
    {
        get => isBought;
        set
        {
            isBought = value;
            PlayerPrefs.SetInt("IsBought" + classData.classType, IsBought ? 1 : 0);
            if (isBought == true)
            {
                InvokeRepeating(nameof(TimePerProducting), 1f, 2f);
                buyBtn.gameObject.SetActive(false);
                sellBtn.gameObject.SetActive(true);
            }
            else if(isBought == false)
            {
                CancelInvoke(nameof(TimePerProducting));
                GameManager.Instance.Effort += classData.currentCost;
                classData.currentCost = classData.buyCost;
                sellBtn.gameObject.SetActive(false);
                buyBtn.gameObject.SetActive(true);
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
    private Button buyBtn;//사는 버튼
    [SerializeField]
    private Button sellBtn;//파는 버튼

    private void Start()
    {
        className.text = $"{classData.className}";
        buyCosttxt.text = $"구매가격: {StringFormat.ToString(classData.buyCost)}";

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
        sellCosttxt.text = $"판매 가격: {StringFormat.ToString(classData.currentCost)}";
    }
}

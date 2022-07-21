using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class ClassRoom : MonoBehaviour
{
    public ClassData classData;
    public bool isCalling;
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt(nameof(IsBought) + classData.classType) == 1)
        {
            isBought = true;
        }
        else
        {
            isBought = false;
        }
    }
    private bool isBought;
    public bool IsBought
    {
        get => isBought;
        set
        {
            isBought = value;
            PlayerPrefs.SetInt(nameof(IsBought) + classData.classType, IsBought ? 1 : 0);
            if (isBought == true)
            {

                buyBtn.gameObject.SetActive(false);
                sellBtn.gameObject.SetActive(true);
            }
            else if (isBought == false)
            {
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
        isCalling = true;
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

    private void Start()
    {
        className.text = $"{classData.className}";
        buyCosttxt.text = $"���Ű���: {StringFormat.ToString(classData.buyCost)}";

        buyBtn.onClick.AddListener(() =>
        {
            IsBought = true;
            if (isCalling == false)
            {
                InvokeRepeating(nameof(TimePerProducting), 1f, 1f);
            }
        });
        sellBtn.onClick.AddListener(() =>
        {
            IsBought = false;
            if (isCalling == true)
            {
                CancelInvoke(nameof(TimePerProducting));
            }
        });
    }
    private void Update()
    {
        sellCosttxt.text = $"�Ǹ� ����: {StringFormat.ToString(classData.currentCost)}";
    }
}

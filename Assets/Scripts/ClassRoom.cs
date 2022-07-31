using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClassRoom : MonoBehaviour
{
    public ClassData classData;
    public bool isCalling;
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt(nameof(IsBought) + classData.classType) == 1)
        {
            isBought = true;
            buyBtn.gameObject.SetActive(false);
            sellBtn.gameObject.SetActive(true);
        }
        else
        {
            isBought = false;
            sellBtn.gameObject.SetActive(false);
            buyBtn.gameObject.SetActive(true);
            classData.currentCost = classData.buyCost;
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

                sellBtn.gameObject.SetActive(false);
                buyBtn.gameObject.SetActive(true);
            }
        }
    }
    public void TimePerProducting()
    {
        if (isBought == true)
        {
            classData.currentCost += classData.timePerSecondProduct;
            isCalling = true;
        }
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
        Debug.Log(isBought);
        className.text = $"{classData.className}";
        buyCosttxt.text = $"구매가격: {StringFormat.ToString(classData.buyCost)}";
        //if (isBought == true)
        //{
        //    InvokeRepeating(nameof(TimePerProducting), 1f, 1f);
        //}
        buyBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button);
            if (GameManager.Instance.Effort >= classData.buyCost)
            {
                IsBought = true;
                print("d");
                GameManager.Instance.Effort -= classData.buyCost;
            }
        });
        sellBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button);
            IsBought = false;
            if (isCalling == true)
            {
                CancelInvoke(nameof(TimePerProducting));
            }
            GameManager.Instance.Effort += classData.currentCost;
            classData.currentCost = classData.buyCost;
        });
    }
    private void Update()
    {
        sellCosttxt.text = $"판매 가격: {StringFormat.ToString(classData.currentCost)}";
    }
}

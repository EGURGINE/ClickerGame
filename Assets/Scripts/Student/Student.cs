using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Student : MonoBehaviour
{
    public StudentData studentData;

    [Header("UIµé")]
    [SerializeField]
    private Image image;
    [SerializeField]
    private Button upGradeBtn;
    [SerializeField]
    private Button grayBtn;

    [Header("Text")]
    [Space(20f)]
    [SerializeField]
    private TextMeshProUGUI leveltxt;
    [SerializeField]
    private TextMeshProUGUI costtxt;
    [SerializeField]
    private TextMeshProUGUI nametxt;
    [SerializeField]
    private TextMeshProUGUI graycosttxt;

    private void Start()
    {
        upGradeBtn.onClick.AddListener(() =>
        {
            if (GameManager.Instance.Effort >= studentData.cost)
            {
                studentData.level += 1;
                GameManager.Instance.Effort -= studentData.cost;
            }
            else
            {

            }
        });
        nametxt.text = studentData.studentName;
    }
    private void Update()
    {
        if(GameManager.Instance.Effort >= studentData.cost)
        {
            grayBtn.gameObject.SetActive(false);
        }
        else
        {
            grayBtn.gameObject.SetActive(true);
        }
        studentData.cost = studentData.baseCost * (ulong)studentData.level;
        leveltxt.text = $"{studentData.level} Level";
        costtxt.text = $"{studentData.cost}";
        graycosttxt.text = $"{studentData.cost}";
        Increment();
    }

    private void Increment()
    {
        studentData.timePerSecondProduct = (ulong)(studentData.level * studentData.productIncrement);
    }
}

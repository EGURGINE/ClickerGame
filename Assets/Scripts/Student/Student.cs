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
    private Button upGradeBtn;
    [SerializeField]
    private Button grayBtn;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private Image image;

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
        image.sprite = sprite;
        OnClickBtns();
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
        Texts();
        Increment();
    }
    private void Texts()
    {
        studentData.cost = (ulong)studentData.level * (ulong)studentData.increment;
        leveltxt.text = $"{studentData.level} Level";
        costtxt.text = $"{StringFormat.ToString(studentData.cost)}";
        graycosttxt.text = $"{StringFormat.ToString(studentData.cost)}";
    }

    private void OnClickBtns()
    {
        upGradeBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button);
            if (GameManager.Instance.Effort >= studentData.cost)
            {
                studentData.level += 1;
                if (studentData.level == 1) CharacterPlacement.Instance.studentLook(studentData);
                GameManager.Instance.Effort -= studentData.cost;
            }
            else
            {

            }
        });
        nametxt.text = studentData.studentName;
    }
    private void Increment()
    {
        studentData.timePerSecondProduct = (ulong)(studentData.level * studentData.productIncrement);
    }
}

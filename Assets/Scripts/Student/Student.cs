using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Student : MonoBehaviour
{
    public StudentData studentData;

    [SerializeField]
    private Image image;
    [SerializeField]
    private Button upGradeBtn;
    [Space(20f)]
    [Header("Text")]
    [SerializeField]
    private TextMeshProUGUI leveltxt;
    [SerializeField]
    private TextMeshProUGUI costtxt;
    [SerializeField]
    private TextMeshProUGUI nametxt;

    private void Start()
    {
        nametxt.text = studentData.studentName;
    }
    private void Update()
    {
        leveltxt.text = $"{studentData.level} Level";
        costtxt.text = $"{studentData.cost}";
    }
}

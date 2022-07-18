using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StudentPresident : MonoBehaviour
{
    [Header("TextMeshPro")]
    [SerializeField]
    private TextMeshProUGUI leveltxt;
    [SerializeField]
    private TextMeshProUGUI costtxt;
    
    [SerializeField]
    [Tooltip("회색 버튼 cost(그냥 cost넣으면됨")]
    private TextMeshProUGUI graybtncosttxt;
    [Tooltip("이름")]
    [SerializeField]
    private TextMeshProUGUI nametxt;

    [Header("버튼")]
    [Space(25f)]
    [SerializeField]
    private Button graybtn;
    [SerializeField]
    private Button upGradeBtn;

    [SerializeField]
    private int level;
    public int Level
    {
        get => level;
        set
        {
            level = value;
        }
    }
    [SerializeField]
    private float cost;
    public float Cost
    {
        get => cost;
        set
        {
            cost = value;
        }
    }
    private void Start()
    {
        upGradeBtn.onClick.AddListener(() =>
        {
            if (cost <= GameManager.Instance.Effort)
            {
                graybtn.gameObject.SetActive(false);
                level += 1;
                GameManager.Instance.Effort -= cost;
            }
            else if (cost > GameManager.Instance.Effort)
            {
                graybtn.gameObject.SetActive(true);
            }
        });
    }

    private void Update()
    {
        leveltxt.text = $"{level}Level";
        graybtncosttxt.text = $"{GetThousandCommaText((int)cost)}";
        costtxt.text = $"{GetThousandCommaText((int)cost)}";
    }
    public string GetThousandCommaText(int data) 
    { 
        return string.Format("{0:#,###}", data); 
    }
}

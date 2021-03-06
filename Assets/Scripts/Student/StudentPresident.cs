using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

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
    [SerializeField]
    private string presidentName;

    [Header("버튼")]
    [Space(25f)]
    [SerializeField]
    private Button graybtn;
    [SerializeField]
    private Button upGradeBtn;

    [SerializeField]
    private int level = 1;
    public int Level
    {
        get => level;
        set
        {
            level = value;
        }
    }
    [SerializeField]
    private ulong cost;
    public ulong Cost
    {
        get => cost;
        set
        {
            cost = value;
        }
    }
    private void Start()
    {
        nametxt.text = $"{presidentName}";

        upGradeBtn.onClick.AddListener(() =>
        {
            if (cost <= GameManager.Instance.Effort)
            {
                level += 1;
                GameManager.Instance.Effort -= cost;
                GameManager.Instance.ClickPerEffortProduct += (ulong)(Level * 0.9);
            }

        });
    }

    private void Update()
    {
        if (cost > GameManager.Instance.Effort)
        {
            graybtn.gameObject.SetActive(true);
        }
        else if (cost <= GameManager.Instance.Effort)
        {
            graybtn.gameObject.SetActive(false);
        }
        leveltxt.text = $"{level}Level";
        graybtncosttxt.text = $"{StringFormat.GetThousandCommaText(cost)}";
        costtxt.text = $"{StringFormat.GetThousandCommaText(cost)}";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class StudentPresident : MonoBehaviour
{
    private const ulong ULONGMAX = 18446744073709551615;
    #region Text
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
    #endregion
    [SerializeField]
    private int increment;

    
    private const float MULTIPLE = 1.6f;

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
            SoundManager.Instance.PlaySound(SoundType.Button);
            Buy();
        });
    }
    private void Buy()
    {
        if (cost <= GameManager.Instance.Effort)
        {
            GameManager.Instance.Effort -= cost;
            print("눌림");
            GameManager.Instance.ClickPerEffortProduct += (ulong)(Level * 0.9);
            level += 1;
        }
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

        if(GameManager.Instance.Effort <= 0)
        {
            Mathf.Clamp(GameManager.Instance.Effort, 0, ULONGMAX);
        }
        cost = (ulong)(increment * level * level * MULTIPLE);
        Texts();
    }
    private void Texts()
    {
        leveltxt.text = $"{level}Level";
        graybtncosttxt.text = $"{StringFormat.GetThousandCommaText(cost)}";
        costtxt.text = $"{StringFormat.GetThousandCommaText(cost)}";
    }
}
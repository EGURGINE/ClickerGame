using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    private const float FEVER = 1.2f;
    #region Texts
    [Header("TextMeshPro")]
    [SerializeField]
    private TextMeshProUGUI effortText;
    [SerializeField]
    private TextMeshProUGUI perClickIncrementText;
    [SerializeField]
    private TextMeshProUGUI perSecondIncrementText;
    #endregion
    [Space(25f)]

    [Header("UI 요소")]
    [SerializeField]
    private Animation effortAnim;//돈 나오는 애니메이션
    [SerializeField]
    private Image effortImage;//돈
    [SerializeField]
    private Button clickArea;//클릭범위
    [SerializeField]
    private Slider feverSlider;//피버 게이지

    public float feverTime;

    private float perClickIncrement;//클릭당 증가량
    public float PerClickIncrement
    {
        get
        {
            return perClickIncrement;
        }
        set
        {
            perClickIncrement = value;
        }
    }

    private float perSecondIncrement;
    public float PerSecondIncrement
    {
        get
        {
            return perSecondIncrement;
        }
        set
        {
            perSecondIncrement = value;
        }
    }



    [SerializeField]
    private float fever;//피버 게이지 값

    [HideInInspector]
    public bool isFeverTime;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        clickArea.onClick.AddListener(() =>
        {
            Click();
        });

        #region 방치 보상
        float nowTime = (DateTime.Now.Hour * 3600) + (DateTime.Now.Minute * 60) + DateTime.Now.Second;
        float OutTime = nowTime - PlayerPrefs.GetFloat("QuitTime");
        Effort += (effortPerSecondProduct * (int)OutTime) * 0.2f;
        #endregion
    }
    private void Update()
    {
        feverSlider.value = fever;
        effortText.text = $"{effort}";
        perClickIncrementText.text = $"{perClickIncrement}";
        perSecondIncrementText.text = $"{perSecondIncrement}";
    }
    private void FixedUpdate()
    {
        FeverTime();
    }
    private void FeverTime()
    {
        while (fever > 0 && isFeverTime == true)
        {
            if (isFeverTime == false && fever <= 0)
            {
                break;
            }
            else
            {
                fever -= Time.deltaTime;
            }
        }
    }
    private void Click()
    {
        Debug.Log(isFeverTime);
        float perClickdeEffortfault = clickPerEffort;
        if (isFeverTime == false)
        {
            effort += perClickdeEffortfault;
            fever += 1;
        }
        else if (isFeverTime == true)
        {
            effort += (perClickIncrement * FEVER);
        }
    }
    #region 싱글톤
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    #endregion

    #region 노력s
    //노력(인게임 재화)
    [SerializeField]
    private float effort;
    public float Effort
    {
        get
        {
            return effort;
        }
        set
        {
            effort = value;
        }
    }

    [SerializeField]
    private float effortPerSecondProduct;//초당 노력(재화)
    public float EffortPerSecondProduct
    {
        get
        {
            return effortPerSecondProduct;
        }
        set
        {
            effortPerSecondProduct = value;
        }
    }
    [SerializeField]
    private float clickPerEffort;//클릭당 실력(재화)
    public float ClickPerEffort
    {
        get
        {
            return clickPerEffort;
        }
        set
        {
            clickPerEffort = value;
        }
    }
    #endregion
}

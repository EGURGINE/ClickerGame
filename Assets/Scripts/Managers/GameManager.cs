using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


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
    private Button clickArea;
    [SerializeField]
    private Slider feverSlider;

    public float feverTime;

    private float perClickIncrement;
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
    private float fever;

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
    }
    private void Update()
    {
        if(isFeverTime == true)
        {
            FeverTime();
        }
    }
    private void FixedUpdate()
    {
        feverSlider.value = fever;
        if (fever > 99)
        {
            isFeverTime = true;
        }
        if (fever < 1)
        {
            isFeverTime = false;
        }

    }
    private void FeverTime()
    {
        fever -= Time.deltaTime;
    }
    private void Click()
    {
        if (isFeverTime == false)
        {
            fever += 1;
        }
        effort += perClickIncrement;
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
    private float effortPerSecondProduct;
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
    private float clickPerEffort;
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

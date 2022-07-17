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

    [Header("UI ���")]
    [SerializeField]
    private Animation effortAnim;//�� ������ �ִϸ��̼�
    [SerializeField]
    private Image effortImage;//��
    [SerializeField]
    private Button clickArea;//Ŭ������
    [SerializeField]
    private Slider feverSlider;//�ǹ� ������

    public float feverTime;

    private float perClickIncrement;//Ŭ���� ������
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
    private float fever;//�ǹ� ������ ��

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

        #region ��ġ ����
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
    #region �̱���
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

    #region ���s
    //���(�ΰ��� ��ȭ)
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
    private float effortPerSecondProduct;//�ʴ� ���(��ȭ)
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
    private float clickPerEffort;//Ŭ���� �Ƿ�(��ȭ)
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

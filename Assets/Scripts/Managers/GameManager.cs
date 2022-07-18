using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    //�ǹ� �������
    private const float FEVER = 1.2f;
    #region Texts
    [Header("TextMeshPro")]
    [SerializeField]
    private TextMeshProUGUI effortText;//���� �Ƿ�(��ȭ)
    [SerializeField]
    private TextMeshProUGUI perClickEffortText;//Ŭ���� �Ƿ�������
    [SerializeField]
    private TextMeshProUGUI perSecondEffortText;//�ʴ� �Ƿ� ������
    #endregion
    #region UI
    [Header("UI ���")]
    [Space(25f)]
    [SerializeField]
    private Animation effortAnim;//�� ������ �ִϸ��̼�
    [SerializeField]
    private Image effortImage;//��
    [SerializeField]
    private Button clickArea;//Ŭ������
    [SerializeField]
    private Slider feverSlider;//�ǹ� ������
    #endregion
    [Space(15f)]
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
    private float clickPerEffortProduct;//Ŭ���� �Ƿ�(��ȭ)
    public float ClickPerEffortProduct
    {
        get
        {
            return clickPerEffortProduct;
        }
        set
        {
            clickPerEffortProduct = value;
        }
    }
    #endregion

    [SerializeField]
    private float fever;//�ǹ� ������ ��

    public float Fever
    {
        get
        {
            return fever;
        }
        set
        {
            fever = value;
            if(fever >= 99)
            {
                isFeverTime = true;
            }
            else if(fever < 2)
            {
                isFeverTime = false;
            }
            feverSlider.value = fever;
        }
    }

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
        
        StartCoroutine(CPerSecondEffortProduct());
        #region ��ġ ����
        float nowTime = (DateTime.Now.Hour * 3600) + (DateTime.Now.Minute * 60) + DateTime.Now.Second;
        float OutTime = nowTime - PlayerPrefs.GetFloat("QuitTime");
        Effort += (effortPerSecondProduct * (int)OutTime) * 0.2f;
        #endregion
    }
    private IEnumerator CPerSecondEffortProduct()
    {
        yield return new WaitForSeconds(1f);
        Effort += effortPerSecondProduct;
        StartCoroutine(CPerSecondEffortProduct());
    }
    private void Update()
    {
        effortText.text = GetThousandCommaText((int)Effort) + "���";
        perClickEffortText.text = GetThousandCommaText((int)clickPerEffortProduct);
        perSecondEffortText.text = GetThousandCommaText((int)effortPerSecondProduct);
    }
    private void FixedUpdate()
    {
        FeverTime();
    }
    private void FeverTime()
    {
        if(isFeverTime == true)
        {
            Fever -= Time.deltaTime * 5f;
        }
    }
    private void Click()
    {
        if (isFeverTime == false)
        {
            effort += clickPerEffortProduct;
            Fever += 1;
        }
        else if (isFeverTime == true)
        {
            effort += (clickPerEffortProduct * FEVER);
        }
        //effortAnim.Play();
    }
    public string GetThousandCommaText(int data)
    {
        return string.Format("{0:#,###}", data);
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
}
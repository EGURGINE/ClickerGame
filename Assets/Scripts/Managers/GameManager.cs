using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Text;

public class GameManager : MonoBehaviour
{
    //�ǹ� �������
    private const float FEVER = 1.2f;
    public List<StudentData> studentDatas = new List<StudentData>();

    [SerializeField]
    private GameObject[] classRoom;
    [SerializeField]
    private GameObject classBuy;

    private const int CLASSCOUNT = 5;

    #region Texts
    [Header("TextMeshPro")]
    [SerializeField]
    private TextMeshProUGUI effortText;//���� �Ƿ�(��ȭ)
    [SerializeField]
    private TextMeshProUGUI perClickEffortText;//Ŭ���� �Ƿ�������
    [SerializeField]
    private TextMeshProUGUI perSecondEffortText;//�ʴ� �Ƿ� ������
    [SerializeField]
    private TextMeshProUGUI neglectTxt;
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
    private ulong effort;
    public ulong Effort
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

    private ulong effortPerSecondProduct;//�ʴ� ���(��ȭ)
    public ulong EffortPerSecondProduct
    {
        get
        {
            #region �ֵ� �� ���ϱ� ��
            ulong a;
            a = studentDatas[0].timePerSecondProduct
                + studentDatas[1].timePerSecondProduct
                + studentDatas[2].timePerSecondProduct
                + studentDatas[3].timePerSecondProduct
                + studentDatas[4].timePerSecondProduct;
            #endregion


            return effortPerSecondProduct = a;
        }
        set
        {
            effortPerSecondProduct = value;
        }
    }
    [SerializeField]
    private ulong clickPerEffortProduct;//Ŭ���� �Ƿ�(��ȭ)
    public ulong ClickPerEffortProduct
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
    #region ����
    [HideInInspector] public bool isBgm;
    [HideInInspector] public bool isSfx;
    [HideInInspector] public bool isEffect;
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
            if (fever >= 99)
            {
                isFeverTime = true;
            }
            else if (fever < 2)
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
        //StartCoroutine(CPerSecondProduct());//�ð��� ����
        classBuy.SetActive(true);
    }

    private void Start()
    {
        classBuy.SetActive(false);

        clickArea.onClick.AddListener(() =>
        {
            Click();
        });

        StartCoroutine(CPerSecondEffortProduct());
        StartCoroutine(CPerSecondProduct());
        //��ġ ���� Neglect();
    }
    private IEnumerator CPerSecondProduct()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < CLASSCOUNT; i++)
        {
            print("aa");
            print(classRoom[i].name);
            classRoom[i].GetComponent<ClassRoom>().TimePerProducting();
        }
        StartCoroutine(nameof(CPerSecondProduct));
    }
    private void Neglect()
    {
        float nowTime = (DateTime.Now.Hour * 3600) + (DateTime.Now.Minute * 60) + DateTime.Now.Second;
        float OutTime = nowTime - PlayerPrefs.GetFloat("QuitTime");
        ulong NeglectCompensation = (effortPerSecondProduct * (ulong)(OutTime * 0.2));
        Effort += NeglectCompensation;
        neglectTxt.text = $"�ʰ� ���� ���� {NeglectCompensation} ��ŭ ����ߴ�...";
    }
    private IEnumerator CPerSecondEffortProduct()
    { 
        yield return new WaitForSeconds(1f);
        Effort += EffortPerSecondProduct;
        StartCoroutine(CPerSecondEffortProduct());
    }
    private void Update()
    {
        effortText.text = StringFormat.GetThousandCommaText(effort) + "���";
        perClickEffortText.text = StringFormat.GetThousandCommaText(clickPerEffortProduct) + "/Ŭ��";
        perSecondEffortText.text = StringFormat.GetThousandCommaText(effortPerSecondProduct) + "/��";

    }
    private void FixedUpdate()//������Ʈ�� for���ֱ� ��
    {
        FeverTime();
    }
    private void FeverTime()
    {
        if (isFeverTime == true)
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
            effort += (ulong)(clickPerEffortProduct * FEVER);
        }
        //effortAnim.Play();
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
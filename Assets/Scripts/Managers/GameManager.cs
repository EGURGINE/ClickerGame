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
    //피버 버프계수
    private const float FEVER = 1.2f;
    public List<StudentData> studentDatas = new List<StudentData>();
    public List<ClassData> classDatas = new List<ClassData>();

    [SerializeField]
    private GameObject[] classRoom;
    [SerializeField]
    private GameObject classBuy;

    private const int CLASSCOUNT = 5;

    #region Texts
    [Header("TextMeshPro")]
    [SerializeField]
    private TextMeshProUGUI effortText;//현재 실력(재화)
    [SerializeField]
    private TextMeshProUGUI perClickEffortText;//클릭당 실력증가량
    [SerializeField]
    private TextMeshProUGUI perSecondEffortText;//초당 실력 증가량
    [SerializeField]
    private TextMeshProUGUI neglectTxt;
    #endregion
    #region UI
    [Header("UI 요소")]
    [Space(25f)]
    [SerializeField]
    private Animation effortAnim;//돈 나오는 애니메이션
    [SerializeField]
    private Image effortImage;//돈
    [SerializeField]
    private Button clickArea;//클릭범위
    [SerializeField]
    private Slider feverSlider;//피버 게이지
    #endregion
    [Space(15f)]
    #region 노력s
    //노력(인게임 재화)
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

    private ulong effortPerSecondProduct;//초당 노력(재화)
    public ulong EffortPerSecondProduct
    {
        get
        {
            #region 애들 다 더하기 ㅋ
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
    private ulong clickPerEffortProduct;//클릭당 실력(재화)
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
    #region 셋팅
    [HideInInspector] public bool isBgm;
    [HideInInspector] public bool isSfx;
    [HideInInspector] public bool isEffect;
    #endregion

    [SerializeField]
    private float fever;//피버 게이지 값

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
        //StartCoroutine(CPerSecondProduct());//시간당 교실
        classBuy.SetActive(true);
        classBuy.SetActive(false);
    }

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {

        }
        clickArea.onClick.AddListener(() =>
        {
            Click();
        });

        StartCoroutine(CPerSecondEffortProduct());
        
        //방치 보상 Neglect();
    }
    private IEnumerator CPerSecondProduct()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < CLASSCOUNT; i++)
        {
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
        neglectTxt.text = $"너가 없던 사이 {NeglectCompensation} 만큼 노력했다...";
    }
    private IEnumerator CPerSecondEffortProduct()
    {
        yield return new WaitForSeconds(1f);
        Effort += EffortPerSecondProduct;
        StartCoroutine(CPerSecondEffortProduct());
    }
    private void Update()
    {
        effortText.text = StringFormat.GetThousandCommaText(effort) + "노력";
        perClickEffortText.text = StringFormat.GetThousandCommaText(clickPerEffortProduct) + "/클릭";
        perSecondEffortText.text = StringFormat.GetThousandCommaText(effortPerSecondProduct) + "/초";

    }
    private void FixedUpdate()//업데이트에 for문넣기 ㅋ
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
}
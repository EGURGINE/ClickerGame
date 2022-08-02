using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Text;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    //피버 버프계수
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
    [SerializeField]
    private TextMeshProUGUI feverText;
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
            print(effort);
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
    [HideInInspector] public bool isBgm = true;
    [HideInInspector] public bool isSfx = true;
    [HideInInspector] public bool isEffect = true;
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

    public GameObject bgm;
    public float quitTime;
    private void Awake()
    {
        instance = this;
        //StartCoroutine(CPerSecondProduct());//시간당 교실
        classBuy.SetActive(true);
    }

    private void Start()
    {
        #region 셋팅
        isBgm = true;
        isSfx = true;
        isEffect = true;
        SoundManager.Instance.PlaySound(SoundType.Bgm);
        #endregion
        classBuy.SetActive(false);

        clickArea.onClick.AddListener(() =>
        {
            Click();
        });

        StartCoroutine(CPerSecondEffortProduct());
        StartCoroutine(CPerSecondProduct());
        StartCoroutine(Neglect());
    }
    private IEnumerator CPerSecondProduct()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < CLASSCOUNT; i++)
        {
            classRoom[i].GetComponent<ClassRoom>().TimePerProducting();
        }
        StartCoroutine(nameof(CPerSecondProduct));
    }
    private IEnumerator Neglect()
    {
        float nowTime = (DateTime.Now.Hour * 3600) + (DateTime.Now.Minute * 60) + DateTime.Now.Second;
        float OutTime = nowTime - quitTime;
        yield return new WaitForSeconds(1f);
        neglectTxt.gameObject.SetActive(true);
        ulong NeglectCompensation = (effortPerSecondProduct * (ulong)(OutTime * 0.2));
        Effort += NeglectCompensation;
        print("dddddd" + NeglectCompensation);
        neglectTxt.text = $"너가 없던 사이 {NeglectCompensation} 만큼 노력했다...";
        neglectTxt.DOFade(0, 5).OnComplete(()=> neglectTxt.gameObject.SetActive(false));
    }
    private IEnumerator CPerSecondEffortProduct()//초당 생산
    {
        yield return new WaitForSeconds(1f);
        Effort += EffortPerSecondProduct;
        StartCoroutine(CPerSecondEffortProduct());
    }
    private void Update()
    {
        TextUpDate();
    }
    private void TextUpDate()
    {
        effortText.text = StringFormat.GetThousandCommaText(effort) + "노력";
        if (isFeverTime == true)
        {
            perClickEffortText.text = StringFormat.GetThousandCommaText(clickPerEffortProduct + clickPerEffortProduct/5) + "/클릭";
        }
        else
        {
            perClickEffortText.text = StringFormat.GetThousandCommaText(clickPerEffortProduct) + "/클릭";
        }
        perSecondEffortText.text = StringFormat.GetThousandCommaText(effortPerSecondProduct) + "/초";
    }
    private void FixedUpdate()
    {
        FeverTime();
    }
    private void FeverTime()
    {
        if (isFeverTime == true)
        {
            Fever -= Time.deltaTime * 5f;
            feverText.gameObject.SetActive(true);
        }
        else
        {
            feverText.gameObject.SetActive(false);
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
        SoundManager.Instance.PlaySound(SoundType.Click);
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
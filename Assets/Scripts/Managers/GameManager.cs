using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    //피버 버프계수
    private const float FEVER = 1.2f;
    public List<StudentData> studentDatas = new List<StudentData>();
    public List<ClassData> classDatas = new List<ClassData>();
    
    #region Texts
    [Header("TextMeshPro")]
    [SerializeField]
    private TextMeshProUGUI effortText;//현재 실력(재화)
    [SerializeField]
    private TextMeshProUGUI perClickEffortText;//클릭당 실력증가량
    [SerializeField]
    private TextMeshProUGUI perSecondEffortText;//초당 실력 증가량
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
    private TextMeshProUGUI neglectTxt;
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
            ulong a;
            a = studentDatas[0].timePerSecondProduct
                + studentDatas[1].timePerSecondProduct
                + studentDatas[2].timePerSecondProduct
                + studentDatas[3].timePerSecondProduct
                + studentDatas[4].timePerSecondProduct;
            
            
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
       //방치 보상 Neglect();
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
        effortText.text = GetThousandCommaText(effort) + "노력";
        perClickEffortText.text = GetThousandCommaText(clickPerEffortProduct) + "/클릭";
        perSecondEffortText.text = GetThousandCommaText(effortPerSecondProduct) + "/초";

    }
    private void FixedUpdate()//업데이트에 for문넣기 ㅋ
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
            effort += (ulong)(clickPerEffortProduct * FEVER);
        }
        //effortAnim.Play();
    }
    public string GetThousandCommaText(ulong data)
    {
        return string.Format("{0:#,###}", data.ToString());
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
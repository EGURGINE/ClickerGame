using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Linq;
using Random = UnityEngine.Random;
public enum EStockType
{
    Class301 = 300000,
    Class305 = 50000,
    Mac = 500000,
    Kineung = 1000,
    Kiup = 150000
}
public enum ECycleTime
{
    Class301 = 900,
    Class305 = 300,
    Mac = 1800,
    Kineung = 60,
    Kiup = 600
}
public class Stock : MonoBehaviour
{
    private const float CMaxRange = 200;
    private const float CMinRange = -200;

    public const float POSYCOUNT = 5f;//Y좌표 개수
    private LineRenderer lineRenderer => GetComponent<LineRenderer>();

    [Header("타입")]
    [SerializeField] private EStockType type;
    [SerializeField] private ECycleTime typeCycle;

    [Header("점")]
    [SerializeField] List<GameObject> dot = new List<GameObject>();

    public List<float> posY = new List<float>(6);

    [Header("비용")]
    [SerializeField] private List<ulong> cost = new List<ulong>();
    [SerializeField] TextMeshProUGUI[] costTxt;

    [Header("제목 / 버튼")]
    [SerializeField] private TextMeshProUGUI subject;
    [SerializeField] private TextMeshProUGUI nowPrice;
    [SerializeField] private Button buyBtn;
    [SerializeField] private Button sellBtn;
    [SerializeField] private Button scaleBtn;
    [SerializeField] private Sprite[] scaleImage;
    private int buyScale = 1;

    public float quitTime;
    #region 소지수
    [Header("소지수")]
    [SerializeField] TextMeshProUGUI haveTxt;
    [SerializeField] private int have;
    public int Have
    {
        get { return have; }
        set
        {
            have = value;
        }
    }
    #endregion
    #region 주기
    [Header("주기")]
    [SerializeField] private Slider cycleDelaySlider;
    private float delay;

    private float cycleDelay;
    public float CycleDelay
    {
        get { return cycleDelay; }
        set
        {
            cycleDelay = value;
            cycleDelaySlider.value = cycleDelay / ((float)typeCycle);
            if (cycleDelay >= (int)typeCycle) Graph();
        }
    }
    #endregion
    private void Awake()
    {
        //Import();
    }
    private void Start()
    {
        #region 버튼
        buyBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button);
            if (GameManager.Instance.Effort > cost.Last() * (ulong)buyScale)
            {
                Have += buyScale;
                GameManager.Instance.Effort -= cost.Last() * (ulong)buyScale;
            }
        });
        sellBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button);
            if (have > 0)
            {
                Have -= buyScale;
                GameManager.Instance.Effort += cost.Last() * (ulong)buyScale;
            }
        });
        scaleBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button);
            switch (buyScale)
            {
                // 이미지 스왑
                case 1:
                    scaleBtn.image.sprite = scaleImage[1];
                    buyScale = 5;
                    break;
                case 5:
                    scaleBtn.image.sprite = scaleImage[2];
                    buyScale = 10;
                    break;
                case 10:
                    scaleBtn.image.sprite = scaleImage[3];
                    buyScale = 50;
                    break;
                case 50:
                    scaleBtn.image.sprite = scaleImage[0];
                    buyScale = 1;
                    break;
                default: Debug.Assert(buyScale > 0);
                    break;
            }
            nowPrice.text = StringFormat.ToString((cost.Last() * (ulong)buyScale));
        });
        #endregion
        subject.text = type.ToString();
        Invoke("Graph", delay);
    }
    private void Update()
    {
        for (int i = 0; i < dot.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(dot[i].transform.position.x, dot[i].transform.position.y, 89.9f)); ;
        }
        haveTxt.text = $"소지수: {have}";
        CycleDelay += Time.deltaTime;
    }
    private void Import()
    {
        Have = PlayerPrefs.GetInt(type + "Have");

        float nowTime = (DateTime.Now.Hour * 3600) + (DateTime.Now.Minute * 60) + DateTime.Now.Second;
        float OutTime = nowTime - PlayerPrefs.GetFloat("QuitTime");

        CycleDelay = PlayerPrefs.GetFloat(type + "CycleDelay");
        if (cycleDelay <= OutTime) delay = 0;
        else delay = ((int)typeCycle) - OutTime;

        for (int i = 0; i < dot.Count; i++)
        {
            if (PlayerPrefs.GetString((int)type + "Cost") == "")
            {
                PlayerPrefs.SetString((int)type + "Cost", "0");
            }
            posY.Add(PlayerPrefs.GetFloat(type + "DotPosY" + i));
            cost.Add(ulong.Parse(PlayerPrefs.GetString(type + "Cost")));
            costTxt[i].text = StringFormat.ToString(cost[i]);
            dot[i].GetComponent<RectTransform>().localPosition += new Vector3(0, posY[i], 0);
        }
        nowPrice.text = StringFormat.ToString((cost.Last() * (ulong)buyScale));
    }
    private void Graph()
    {
        CycleDelay = 0;
        for (int i = 0; i < dot.Count; i++)
        {
            if (i >= dot.Count - 1) posY[i] = Random.Range(CMinRange, CMaxRange);
            else posY[i] = posY[i + 1];

            CostCalculation(i);
        }
        for (int i = 0; i < dot.Count; i++) dot[i].GetComponent<RectTransform>().localPosition =
                new Vector3(dot[i].GetComponent<RectTransform>().localPosition.x, posY[i], 0);
        nowPrice.text = StringFormat.ToString((cost[cost.Count - 1] * (ulong)buyScale));
    }
    private void CostCalculation(int _num)
    {
        switch (type)
        {
            case EStockType.Class301:
                cost[_num] = (ulong)type + ((ulong)posY[_num] * 30);
                break;
            case EStockType.Class305:
                cost[_num] = (ulong)type + ((ulong)posY[_num] * 10);
                break;
            case EStockType.Mac:
                cost[_num] = (ulong)type + ((ulong)posY[_num] * 100);
                break;
            case EStockType.Kineung:
                cost[_num] = (ulong)type + (ulong)posY[_num];
                break;
            case EStockType.Kiup:
                cost[_num] = (ulong)type + ((ulong)posY[_num] * 50);
                break;
        }
        costTxt[_num].text = StringFormat.ToString(cost[_num]);
    }
    private void OnApplicationQuit()
    {
        //Save();
    }
    private void Save()
    {
        for (int i = 0; i < dot.Count; i++)
        {
            PlayerPrefs.SetFloat(type + "DotPosY" + i, posY[i]);
            PlayerPrefs.SetString(type + "Cost", cost[i].ToString());
        }
        float quitTime = (DateTime.Now.Hour * 3600) + (DateTime.Now.Minute * 60) + DateTime.Now.Second;
        PlayerPrefs.SetFloat("QuitTime", quitTime);
        PlayerPrefs.SetFloat(type + "CycleDelay", cycleDelay);
        PlayerPrefs.SetInt(type + "Have", have);
    }
}

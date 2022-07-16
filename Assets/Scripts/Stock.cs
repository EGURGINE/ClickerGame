using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
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
    Class301 = 10,
    Class305 = 300,
    Mac = 600,
    Kineung = 900,
    Kiup = 1800
}
public class Stock : MonoBehaviour
{
    private const float CMaxRange = 200;
    private const float CMinRange = -200;
    private LineRenderer lineRenderer => GetComponent<LineRenderer>();

    [Header("타입")]
    [SerializeField] private EStockType type;
    [SerializeField] private ECycleTime typeCycle;

    [Header("점")]
    [SerializeField] List<GameObject> dot = new List<GameObject>();
    [SerializeField] List<float> posY = new List<float>();

    [Header("비용")]
    [SerializeField] private List<int> cost = new List<int>();
    [SerializeField] TextMeshProUGUI[] costTxt;

    [Header("제목 / 버튼")]
    [SerializeField] private TextMeshProUGUI subject;
    [SerializeField] private Button buyBtn;
    [SerializeField] private Button sellBtn;

    #region 소지수
    [Header("소지수")]
    [SerializeField] TextMeshProUGUI haveTxt;
    [SerializeField] private int have;
    private int Have
    {
        get { return have; }
        set
        {
            have = value;
            haveTxt.text = have.ToString();
        }
    }
    #endregion

    #region 주기
    [Header("주기")]
    [SerializeField] private Slider cycleDelaySlider;
    private float delay;

    private float cycleDelay;
    private float CycleDelay
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
    private void Start()
    {
        #region 버튼
        subject.text = type.ToString();
        buyBtn.onClick.AddListener(() =>
        {
            if (GameManager.Instance.Effort > cost[cost.Count - 1])
            {
                Have++;
                GameManager.Instance.Effort -= (float)cost[cost.Count - 1];
            }
        });

        sellBtn.onClick.AddListener(() =>
        {
            if (have > 0)
            {
                Have--;
                GameManager.Instance.Effort += (float)cost[cost.Count - 1];
            }
        });
        #endregion
        Import();
        Invoke("Graph", delay);
    }
    private void Update()
    {
        for (int i = 0; i < dot.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(dot[i].transform.position.x, dot[i].transform.position.y, 0));
        }

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
            posY.Add(PlayerPrefs.GetFloat("DotPosY" + i));
            cost.Add((int)type - ((int)posY[i] * 10));
            dot[i].GetComponent<RectTransform>().localPosition += new Vector3(0, posY[i], 0);
        }
    }
    private void Graph()
    {
        CycleDelay = 0;
        for (int i = 0; i < dot.Count; i++)
        {
            if (i >= dot.Count - 1) posY[i] = UnityEngine.Random.Range(CMinRange, CMaxRange);
            else posY[i] = posY[i + 1];

            cost[i] = ((int)type - ((int)posY[i] * 10));
            costTxt[i].text = cost[i].ToString();
        }
        for (int i = 0; i < dot.Count; i++) dot[i].GetComponent<RectTransform>().localPosition =
                new Vector3(dot[i].GetComponent<RectTransform>().localPosition.x, posY[i], 0);
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    private void Save()
    {
        for (int i = 0; i < dot.Count; i++) PlayerPrefs.SetFloat("DotPosY" + i, posY[i]);

        float time = (DateTime.Now.Hour * 3600) + (DateTime.Now.Minute * 60) + DateTime.Now.Second;
        PlayerPrefs.SetFloat("QuitTime", time);
        PlayerPrefs.SetFloat(type + "CycleDelay", cycleDelay);

        PlayerPrefs.SetInt(type + "Have", have);
    }
}

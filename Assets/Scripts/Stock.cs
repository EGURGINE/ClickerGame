using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Linq;
using Random = UnityEngine.Random;
public enum ECycleTime
{
    Kineung = 30,
    Class305 = 75,
    Mac = 120,
    Class301 = 150,
    Kiup = 210
}
public enum EStockType
{
    Kineung = 100000,//10��
    Class305 = 1000000,//100��
    Mac = 10000000,//1000��
    Class301 = 500000000,//5��
    Kiup = 2000000000//20�� * 10
}
public enum EStockLowestValue
{
    Kineung = 50000,//5��
    Class305 = 500000,//50��
    Mac = 2000000,//200��
    Class301 = 100000000,//1��
    Kiup = 1000000000//10�� * 5
}
public enum EStockMaximumValue
{
    Kineung = 200000,//20��
    Class305 = 3000000,//300��
    Mac = 50000000,//5000��
    Class301 = 2000000000,//20��
    Kiup = 1000000000//10�� * 50;
}
public class Stock : MonoBehaviour
{
    #region ����
    private const int CMaxRange = 200;
    private const int CMinRange = -200;

    public const float POSYCOUNT = 5f;//Y��ǥ ����
    private LineRenderer lineRenderer => GetComponent<LineRenderer>();

    [Header("Ÿ��")]
    [SerializeField] private EStockType type;
    [SerializeField] private ECycleTime typeCycle;
    [SerializeField] private EStockLowestValue stockLowestValue;
    [SerializeField] private EStockMaximumValue stockMaximumValue;

    [Header("��")]
    [SerializeField] List<GameObject> dot = new List<GameObject>();
    public List<int> posY = new List<int>(6);

    #endregion
    #region ����
    [Header("���")]
    [SerializeField] private List<ulong> cost = new List<ulong>();
    [SerializeField] TextMeshProUGUI[] costTxt;
    ulong isCost;
    ulong costScale;
    ulong costLowestimum;
    #endregion
    #region UI
    [Header("���� / ��ư")]
    [SerializeField] private TextMeshProUGUI subject;
    [SerializeField] private TextMeshProUGUI nowPrice;
    [SerializeField] private Button buyBtn;
    [SerializeField] private Button sellBtn;
    [SerializeField] private Button scaleBtn;
    [SerializeField] private Sprite[] scaleImage;
    private int buyScale = 1;
    #endregion
    #region ������
    [Header("������")]
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
    #region �ֱ�
    [Header("�ֱ�")]
    [SerializeField] private Slider cycleDelaySlider;
    private float delay;
    public float quitTime;

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
    private void Start()
    {
        if (type == EStockType.Kiup) costScale = (((ulong)stockMaximumValue * 50) - ((ulong)stockLowestValue)*5) / 400;
        else costScale = ((ulong)stockMaximumValue - (ulong)stockLowestValue)  / 400;

         
        print( type + "������ : " + costScale);
        #region ��ư
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
                // �̹��� ����
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
        haveTxt.text = $"������: {have}";
        CycleDelay += Time.deltaTime;
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

        if (posY[_num] > 0)
        {
            isCost = (ulong)(200 + posY[_num]) * costScale;
        }
        else
        {
            isCost = (ulong)(200 + posY[_num]) * costScale;
        }

        if (isCost == 0) isCost += (ulong)stockLowestValue;
        cost[_num] =  isCost;
        costTxt[_num].text = StringFormat.ToString(cost[_num]);
    }
}

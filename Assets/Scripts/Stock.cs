using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public enum EStockType
{
    Class301 = 300000,
    Class305 = 50000,
    Mac = 500000,
    Kineung = 1000,
    Kiup = 150000
}
public class Stock : MonoBehaviour 
{
    private const float CMaxRange = 200;
    private const float CMinRange = -200;

    [SerializeField] private EStockType type;
    [SerializeField] List<GameObject> dot = new List<GameObject>();
    [SerializeField] List<float> posY = new List<float>();
    [SerializeField] private int[] cost;
    [SerializeField] TextMeshProUGUI[] costTxt;
    private LineRenderer lineRenderer=>GetComponent<LineRenderer>();
    private void Start()
    {
        for (int i = 0; i < dot.Count; i++)
        {
            posY.Add(PlayerPrefs.GetFloat("DotPosY" + i));
            dot[i].GetComponent<RectTransform>().localPosition += new Vector3(0, posY[i], 0);
        }
        switch (type)
        {
            case EStockType.Class301:
                InvokeRepeating("Graph", 0, 60f);
                break;
            case EStockType.Class305:
                InvokeRepeating("Graph", 0, 300f);
                break;
            case EStockType.Mac:
                InvokeRepeating("Graph", 0, 600f);
                break;
            case EStockType.Kineung:
                InvokeRepeating("Graph", 0, 900f);
                break;
            case EStockType.Kiup:
                InvokeRepeating("Graph", 0, 1800f);
                break;
            
        }
    }

    private void Graph()
    {
        for (int i = 0; i < dot.Count; i++)
        {
            if(i >= dot.Count - 1)
            {
                posY[i] = UnityEngine.Random.Range(CMinRange, CMaxRange);
            }
            else
            {
                posY[i] = posY[i+1];
            }
            cost[i] = (int)posY[i] * (int)type;
            costTxt[i].text = cost[i].ToString();
        }
        for (int i = 0; i < dot.Count; i++)
        {
            PlayerPrefs.SetFloat("DotPosY" + i, posY[i]); // 나중에 게임 나가기로 이동
            dot[i].GetComponent<RectTransform>().localPosition = new Vector3(dot[i].GetComponent<RectTransform>().localPosition.x, posY[i], 0);
            lineRenderer.SetPosition(i, new Vector3(dot[i].transform.position.x, dot[i].transform.position.y, 0));
        }
    }
    public void BuyBtn()
    {
        //돈 깎기
    }
    public void SellBtn()
    {
        // 돈 넣기
        // posY[-1] * 갯수 * 기본 가격
    }
}

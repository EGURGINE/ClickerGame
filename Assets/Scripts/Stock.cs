using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum EStockType
{
    SMALL,
    NORMAL,
    HIGH
}
public class Stock : MonoBehaviour 
{
    private const float CMaxRange = 200;
    private const float CMinRange = -200;

    [SerializeField] private EStockType type;
    [SerializeField] List<GameObject> dot = new List<GameObject>();
    [SerializeField] List<float> posY = new List<float>();

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
            case EStockType.HIGH:
                InvokeRepeating("Graph", 0,600f);
                break;
            case EStockType.NORMAL:
                InvokeRepeating("Graph", 0, 350f);
                break;
            case EStockType.SMALL:
                InvokeRepeating("Graph", 0, 60f);
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
        }
        for (int i = 0; i < dot.Count; i++)
        {
            PlayerPrefs.SetFloat("DotPosY" + i, posY[i]); // 나중에 게임 나가기로 이동
            dot[i].GetComponent<RectTransform>().localPosition = new Vector3(dot[i].GetComponent<RectTransform>().localPosition.x, posY[i], 0);
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

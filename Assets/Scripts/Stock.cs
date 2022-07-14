using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum EStockType
{
    HIGH = 5,
    NORMAL = 3,
    SMALL = 1
}
public class Stock : MonoBehaviour 
{ 
    [SerializeField] private EStockType type;
    [SerializeField] List<GameObject> dot = new List<GameObject>();
    [SerializeField] List<float> posY = new List<float>();
    Dictionary<GameObject, float> dotPos = new Dictionary<GameObject, float>();
    private LineRenderer lineRenderer;

    private void Start()
    {
        for (int i = 0; i < dot.Count; i++)
        {
            posY.Add(PlayerPrefs.GetFloat("DotPosY" + i));
            dotPos.Add(dot[i], posY[i]);
        }
        switch (type)
        {
            case EStockType.HIGH:
                InvokeRepeating("Graph", 600f,600f);
                break;
            case EStockType.NORMAL:
                InvokeRepeating("Graph", 350f, 350f);
                break;
            case EStockType.SMALL:
                InvokeRepeating("Graph", 60f, 60f);
                break;
        }
    }

    private void Graph()
    {
        for (int i = 0; i < dot.Count; i++)
        {
            if(i == posY.Count)
            {
                posY[i] += UnityEngine.Random.Range(-(float)type, (float)type);
            }
            else
            {
                posY[i] = posY[i++];
            }
        }
        for (int i = 0; i < dot.Count; i++)
        {
            PlayerPrefs.SetFloat("DotPosY" + i, posY[i]); // 나중에 게임 나가기로 이동
            dotPos[dot[i]] = posY[i];
            dot[i].transform.position += new Vector3(0, dotPos[dot[i]],0);
            dot[i].GetComponent<LineRenderer>().SetPosition(0, dot[i].transform.position);
            dot[i].GetComponent<LineRenderer>().SetPosition(1,dot[i++].transform.position);
        }
    }
}

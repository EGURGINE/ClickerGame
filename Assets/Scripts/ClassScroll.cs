using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class ClassScroll : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private readonly int minRange = 500;
    [SerializeField] private GameObject Maps;
    float startPosX;
    int isMapNum = 0;
    [SerializeField] private GameObject[] mapObjs;
    [SerializeField] private GameObject mapLight;
    public void OnPointerDown(PointerEventData eventData)
    {
        startPosX = Input.mousePosition.x;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        float mouseUpPos = Input.mousePosition.x;
        if (mouseUpPos <= startPosX - minRange)
        {
            if (isMapNum >= 4) return;
            else isMapNum++;
        }
        else if (mouseUpPos >= startPosX + minRange)
        {
            if (isMapNum <= 0) return;
            else isMapNum--;
        }
        else return;
        mapLight.SetActive(false);
        Maps.transform.DOLocalMoveX(isMapNum * -60, 1f).OnComplete(() =>
        {
            if (mapObjs[isMapNum].GetComponent<ClassRoom>().IsBought)
            {
                mapLight.SetActive(true);
            }
            else mapLight.SetActive(false);
        });
    }
}

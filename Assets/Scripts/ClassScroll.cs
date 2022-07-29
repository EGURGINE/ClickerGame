using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class ClassScroll : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private readonly int minRange = 50;
    [SerializeField] private GameObject Maps;
    float startPosX;
    int isMapNum = 1;
    public void OnPointerDown(PointerEventData eventData)
    {
        startPosX = Input.mousePosition.x;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        float mouseUpPos = Input.mousePosition.x;
        if (mouseUpPos <= startPosX - minRange)
        {
            if (isMapNum >= 5) return;
            else isMapNum++;
            switch (isMapNum)
            {
                case 1:
                    Maps.transform.DOLocalMoveX(0, 1f);
                    break;
                case 2:
                    Maps.transform.DOLocalMoveX(-60, 1f);
                    break;
                case 3:
                    Maps.transform.DOLocalMoveX(-120, 1f);
                    break;
                case 4:
                    Maps.transform.DOLocalMoveX(-180, 1f);
                    break;
                case 5:
                    Maps.transform.DOLocalMoveX(-240, 1f);
                    break;
            }
        }
        else if (mouseUpPos >= startPosX + minRange)
        {
            if (isMapNum <= 1) return;
            else isMapNum--;
            switch (isMapNum)
            {
                case 1:
                    Maps.transform.DOLocalMoveX(0, 1f);
                    break;
                case 2:
                    Maps.transform.DOLocalMoveX(-60, 1f);
                    break;
                case 3:
                    Maps.transform.DOLocalMoveX(-120, 1f);
                    break;
                case 4:
                    Maps.transform.DOLocalMoveX(-180, 1f);
                    break;
                case 5:
                    Maps.transform.DOLocalMoveX(-240, 1f);
                    break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class Scroll : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Vector2 LeftUpPos;
    [SerializeField] Vector2 RightDownPos;
    [SerializeField] private int minRange = 500;
    [SerializeField] private GameObject scrollObjs;
    Vector2 mousePos;
    int isMapNum = 0;
    [SerializeField] float scrollScale;
    public void OnPointerDown(PointerEventData eventData)
    {
        mousePos = Input.mousePosition;
        print(Input.mousePosition);
    }
    private void Check()
    {
        if (mousePos.y < LeftUpPos.y && mousePos.y > RightDownPos.y)
        {
            float mouseUpPos = Input.mousePosition.x;
            if (mouseUpPos <= mousePos.x - minRange)
            {
                if (isMapNum >= 4) return;
                else isMapNum++;
            }
            else if (mouseUpPos >= mousePos.x + minRange)
            {
                if (isMapNum <= 0) return;
                else isMapNum--;
            }
            else return;
            scrollObjs.transform.DOLocalMoveX(isMapNum * scrollScale, 1f);
        };

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Check();
    }
}

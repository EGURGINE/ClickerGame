using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ClassScroll : MonoBehaviour
{
    float startPos;
    private void OnMouseDown()
    {
        startPos = Input.mousePosition.x;
    }
    private void OnMouseUp()
    {
        float mouseUpPos = Input.mousePosition.x;
        if (mouseUpPos >= startPos + 50)
        {

        }
        else if(mouseUpPos <= startPos - 50)
        {

        }
    }
}

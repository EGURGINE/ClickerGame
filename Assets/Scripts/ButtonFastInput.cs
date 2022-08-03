using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EButtonType
{
    Student,
    StudentPresident
}
public class ButtonFastInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressing;
    public const float MAXPERCLICKSECOND = 1f;
    private const float UPGRADECLICKTIME = 0.01f;

    private float clickSecond;
    private EButtonType buttonType;
    private Student student;
    private StudentPresident presidentstu;
    private void Start()
    {
        if (student != null)
        {
            buttonType = EButtonType.Student;
        }
        else if (presidentstu != null)
        {
            buttonType = EButtonType.StudentPresident;
        }
    }

    private void Update()
    {
        if(isPressing == true)
        {
            WaitFastClick();
        }
        else if(isPressing == false)
        {
            StopCoroutine(CFastUpGrade());
        }
    }

    /// <summary>
    /// 빠르게 업그래이드 하기 전에 대기하는 함수
    /// </summary>
    private void WaitFastClick()
    {
        clickSecond += Time.deltaTime;
        if (clickSecond >= MAXPERCLICKSECOND)
        {

        }
    }
    private IEnumerator CFastUpGrade()
    {
        yield return new WaitForSeconds(UPGRADECLICKTIME);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressing = false;
    }
}

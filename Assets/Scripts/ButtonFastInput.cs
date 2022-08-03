using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PresidentButtonFastInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressing;
    public const float MAXPERCLICKSECOND = 1f;
    private const float UPGRADECLICKTIME = 0.1f;

    private float clickSecond;
    private StudentPresident presidentstu;

    private GameManager gm;

    private void Awake()
    {
        gm = GameManager.Instance;
    }
    private void Start()
    {
        presidentstu = GetComponent<StudentPresident>();
    }

    private void FixedUpdate()
    {
        if (isPressing == true)
        {
            WaitFastClick();
        }
        else if (isPressing == false)
        {
            StopCoroutine(CFastUpGrade());
            clickSecond = 0;
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
            StartCoroutine(CFastUpGrade());
        }
    }
    private IEnumerator CFastUpGrade()
    {
        yield return new WaitForSeconds(UPGRADECLICKTIME);
        FastClick();
    }
    private void FastClick()
    {
        if (gm.Effort >= presidentstu.Cost)
        {
            gm.Effort -= presidentstu.Cost;
        }
        else
        {
            Debug.Log("돈없이 임마");
        }

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

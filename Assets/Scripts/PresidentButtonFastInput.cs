using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PresidentButtonFastInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressing;//누르고 있냐
    public const float WAITCLICKSECOND = 1f;//버튼 누르고 있어야 되는 시간
    private const float UPGRADECLICKTIME = 0.1f;//얼마 마다 눌릴거냐

    private float clickSecond;
    [SerializeField]
    private StudentPresident presidentstu;

    private GameManager gm;

    private void Awake()
    {
        gm = GameManager.Instance;
    }
    private void Start()
    {
        
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
        if (clickSecond >= WAITCLICKSECOND)
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
            presidentstu.Level += 1;
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

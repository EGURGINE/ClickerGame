using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PresidentButtonFastInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressing;//������ �ֳ�
    public const float WAITCLICKSECOND = 1f;//��ư ������ �־�� �Ǵ� �ð�
    private const float UPGRADECLICKTIME = 0.1f;//�� ���� �����ų�

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
    /// ������ ���׷��̵� �ϱ� ���� ����ϴ� �Լ�
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
            Debug.Log("������ �Ӹ�");
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

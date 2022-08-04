using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StudentButtonFastInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressing;//������ �ֳ�
    public const float WAITCLICKSECOND = 1f;//��ư ������ �־�� �Ǵ� �ð�
    private const float UPGRADECLICKTIME = 0.1f;//�� ���� �����ų�

    private float clickSecond;
    [SerializeField]
    private Student student;

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
        if (gm.Effort >= student.studentData.cost)
        {
            gm.Effort -= student.studentData.cost;
            student.studentData.level += 1;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private const float FEVER = 1.2f;
    #region Texts
    [SerializeField]
    private TextMeshProUGUI effortText;
    [SerializeField]
    private TextMeshProUGUI perClickIncrementText;
    [SerializeField]
    private TextMeshProUGUI perSecondIncrementText;
    #endregion

    [SerializeField]
    private Animation effortAnim;//�� ������ �ִϸ��̼�
    [SerializeField]
    private Image effortImage;//��


    private float perClickIncrement;
    public float PerClickIncrement
    {
        get
        {
            return perClickIncrement;
        }
        set
        {
            perClickIncrement = value;
        }
    }

    private float perSecondIncrement;
    public float PerSecondIncrement
    {
        get
        {
            return perSecondIncrement;
        }
        set
        {
            perSecondIncrement = value;
        }
    }

    [SerializeField]
    private Button click;

    [SerializeField]
    private float fever;

    public bool isFeverTime;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        click.onClick.AddListener(() =>
        {
            Click();
        });
    }
    private void Update()
    {
        if (isFeverTime == false)
            fever += Time.deltaTime;

        if (fever == 100f)
        {
            isFeverTime = true;
        }
        if (isFeverTime == true)
        {
            StartCoroutine(FeverTime());
        }
    }
    private IEnumerator FeverTime()
    {
        while (fever > 0)
        {
            fever -= Time.deltaTime;
            isFeverTime = true;
        }
        yield return null;
    }
    private void Click()
    {
        effort += perClickIncrement;
        //����Ʈ
    }

    #region �̱���
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    #endregion

    #region ���s
    //���(�ΰ��� ��ȭ)
    [SerializeField]
    private float effort;
    public float Effort
    {
        get
        {
            return effort;
        }
        set
        {
            effort = value;
        }
    }
    [SerializeField]
    private float effortPerSecondProduct;
    public float EffortPerSecondProduct
    {
        get
        {
            return effortPerSecondProduct;
        }
        set
        {
            effortPerSecondProduct = value;
        }
    }
    [SerializeField]
    private float clickPerEffort;
    public float ClickPerEffort
    {
        get
        {
            return clickPerEffort;
        }
        set
        {
            clickPerEffort = value;
        }
    }
    #endregion
}

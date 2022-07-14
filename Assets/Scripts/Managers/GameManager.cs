using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private const float FEVER = 1.2f;
    [SerializeField]
    private TextMeshProUGUI effortText;
    [SerializeField]
    private TextMeshProUGUI perClickIncrementText;
    [SerializeField]
    private TextMeshProUGUI perSecondIncrementText;

    private float perClickIncrementdefault;
    
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
    private float perSecondIncrementdefault;
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
        perClickIncrement = perClickIncrementdefault;
        perSecondIncrement = perSecondIncrementdefault;

        click.onClick.AddListener(() =>
        {
            Click();
        });
    }
    private void Update()
    {
        if (isFeverTime == false)
        fever += Time.deltaTime;

        if(fever >= 100)
        {
            isFeverTime = true;
        }
        if(isFeverTime == true)
        {
            StartCoroutine(FeverTime());
        }
    }
    private IEnumerator FeverTime()
    {
        while(fever > 0)
        {
            fever -= Time.deltaTime;
            isFeverTime = true;
        }
        yield return null;
    }
    private IEnumerator ChargeFever()
    {
        yield return null;
    }
    private void Click()
    {
        effort += perClickIncrement;
        //¿Ã∆Â∆Æ
    }

    #region ΩÃ±€≈Ê
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
    //≥Î∑¬(¿Œ∞‘¿” ¿Á»≠)
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
}

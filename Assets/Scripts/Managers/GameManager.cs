using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text effortText;
    [SerializeField]
    private Text perClickIncrementText;
    [SerializeField]
    private Text perSecondIncrementText;

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
            return perClickIncrement;
        }
        set
        {
            perClickIncrement = value;
        }
    }

    [SerializeField]
    private Button click;

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

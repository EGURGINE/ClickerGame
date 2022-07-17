using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StudentPresident : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI leveltxt;
    [SerializeField]
    private TextMeshProUGUI costtxt;
    [SerializeField]
    private TextMeshProUGUI nametxt;
    [SerializeField]
    private Button upGradeBtn;

    [SerializeField]
    private int level;
    public int Level
    {
        get => level;
        set
        {
            level = value;
        }
        
    }
    [SerializeField]
    private float cost;
    public float Cost
    {
        get => cost;
        set
        {
            cost = value;
        }
    }
    private void Start()
    {
        upGradeBtn.onClick.AddListener(() =>
        {
    
        });
    }

    private void Update()
    {
        leveltxt.text = $"{level}";
    }
}

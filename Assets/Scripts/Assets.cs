using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Assets : MonoBehaviour
{
    [SerializeField]
    private ClassData classData;
    [SerializeField]
    private Sprite img;

    private ClassRoom classroom;
    private Image picture;

    #region TextMeshPro
    [Header("TextMeshPro")]
    [Space(10f)]
    [SerializeField]
    private TextMeshProUGUI buyCost;
    [SerializeField]
    private TextMeshProUGUI currentCost;
    [SerializeField]
    private TextMeshProUGUI className;
    #endregion

    private void Start()
    {
        picture = GetComponent<Image>();
        buyCost.text = StringFormat.ToString(classData.buyCost);
        className.text = $"{classData.className}";
        //picture.sprite = img;

        Debug.Assert(classroom == null, "classroom is null");
    }

    private void Update()
    {
        currentCost.text = StringFormat.ToString(classData.currentCost);
    }
}

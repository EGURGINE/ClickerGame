using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Assets : MonoBehaviour
{
    [SerializeField]
    private Sprite img;

    [SerializeField]
    private GameObject classRoomObj;

    private ClassRoom classRoom;

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
        classRoom = classRoomObj.GetComponent<ClassRoom>();
        picture = GetComponent<Image>();
        buyCost.text = StringFormat.ToString(classRoom.classData.buyCost);
        className.text = $"{classRoom.classData.className}";
        //picture.sprite = img;
    }

    private void Update()
    {
        currentCost.text = StringFormat.ToString(classRoom.classData.currentCost);
    }
}

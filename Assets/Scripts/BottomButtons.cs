using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SF = UnityEngine.SerializeField;
using UnityEngine.UI;

public class BottomButtons : MonoBehaviour
{
    [SF] private Button[] bottomBtns;
    [SF] private GameObject[] bottomBords;
    [SF] private Button[] ExitBtn;
    private void Start()
    {
        for (int i = 0; i < bottomBtns.Length; i++)
        {
            int index = i;
            bottomBtns[index].onClick.AddListener(() => bottomBords[index].SetActive(true));
        }

        for (int i = 0; i < ExitBtn.Length; i++)
        {
            int index = i;
            ExitBtn[index].onClick.AddListener(() =>
            {
                for (int i = 0; i < bottomBords.Length; i++)
                {
                    bottomBords[i].SetActive(false);
                }
            });

        }

        
    }
}

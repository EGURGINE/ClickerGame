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
            bottomBtns[index].onClick.AddListener(() =>
            {
                bottomBords[index].SetActive(true);
                SoundManager.Instance.PlaySound(SoundType.Button);
            });
        }

        for (int i = 0; i < ExitBtn.Length; i++)
        {
            ExitBtn[i].onClick.AddListener(() =>
            {
                bottomBords[i].SetActive(false);
                SoundManager.Instance.PlaySound(SoundType.Button);
            });
        }
    }
}

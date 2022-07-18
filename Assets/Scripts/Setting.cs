using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Setting : MonoBehaviour
{
    [SerializeField] private Button Bgm;
    [SerializeField] private Button Sfx;
    [SerializeField] private Button Effect;
    void Start()
    {
        Bgm.onClick.AddListener(() =>
        {
            switch (GameManager.Instance.isBgm)
            {
                case true:
                    GameManager.Instance.isBgm = false;
                    //�̹��� ����
                    break;
                case false:
                    GameManager.Instance.isBgm = true;
                    //�̹��� ����
                    break;
            }
        });
        Sfx.onClick.AddListener(() =>
        {
            switch (GameManager.Instance.isSfx)
            {
                case true:
                    GameManager.Instance.isSfx = false;
                    //�̹��� ����
                    break;
                case false:
                    GameManager.Instance.isSfx = true;
                    //�̹��� ����
                    break;
            }
        });
        Effect.onClick.AddListener(() =>
        {
            switch (GameManager.Instance.isEffect)
            {
                case true:
                    GameManager.Instance.isEffect = false;
                    //�̹��� ����
                    break;
                case false:
                    GameManager.Instance.isEffect = true;
                    //�̹��� ����
                    break;
            }
        });
    }
}

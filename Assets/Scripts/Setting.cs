using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Setting : MonoBehaviour
{
    [SerializeField] private GameObject SetWnd;
    [Space(10f)]
    [Header("��ư")]
    [SerializeField] private Button SettingOnBtn;
    [SerializeField] private Button Exit;
    [SerializeField] private Button Bgm;
    [SerializeField] private Button Sfx;
    [SerializeField] private Button Effect;
    private void Start()
    {
        SettingOnBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button);
            SetWnd.SetActive(true);
        });
        Exit.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button);
            print("?");
            SetWnd.SetActive(false);
        });
        Bgm.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundType.Button);
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
            SoundManager.Instance.PlaySound(SoundType.Button);
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
            SoundManager.Instance.PlaySound(SoundType.Button);
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

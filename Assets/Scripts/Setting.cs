using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Setting : MonoBehaviour
{
    [SerializeField] private GameObject SetWnd;
    [Space(10f)]
    [Header("버튼")]
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
                    //이미지 스왑
                    break;
                case false:
                    GameManager.Instance.isBgm = true;
                    //이미지 스왑
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
                    //이미지 스왑
                    break;
                case false:
                    GameManager.Instance.isSfx = true;
                    //이미지 스왑
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
                    //이미지 스왑
                    break;
                case false:
                    GameManager.Instance.isEffect = true;
                    //이미지 스왑
                    break;
            }
        });
    }
}

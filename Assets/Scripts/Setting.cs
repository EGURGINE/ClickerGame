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

    [SerializeField] private Sprite BgmOnImg;
    [SerializeField] private Sprite BgmOffImg;
    [SerializeField] private Sprite SfxOnImg;
    [SerializeField] private Sprite SfxOffImg;
    [SerializeField] private Sprite EffectOnImg;
    [SerializeField] private Sprite EffectOffImg;


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
                    Destroy(GameManager.Instance.bgm);
                    Bgm.image.sprite = BgmOffImg;
                    //이미지 스왑
                    break;
                case false:
                    GameManager.Instance.isBgm = true;
                    SoundManager.Instance.PlaySound(SoundType.Bgm);
                    Bgm.image.sprite = BgmOnImg;
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
                    Sfx.image.sprite = SfxOffImg;
                    //이미지 스왑
                    break;
                case false:
                    GameManager.Instance.isSfx = true;
                    Sfx.image.sprite = SfxOnImg;
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
                    Effect.image.sprite = EffectOffImg;
                    //이미지 스왑
                    break;
                case false:
                    GameManager.Instance.isEffect = true;
                    Effect.image.sprite = EffectOnImg;
                    //이미지 스왑
                    break;
            }
        });
    }
}

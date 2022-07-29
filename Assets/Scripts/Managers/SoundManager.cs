using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Bgm,
    Click,
    Button,
    End
}
public class SoundManager : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }
            return instance;
        }
    }
    #endregion
    [SerializeField] private List<AudioClip> clips; 

    private Dictionary<SoundType,float> soundVolume = new Dictionary<SoundType, float>();
    private void Awake()
    {
        instance = this;
        soundVolume.Add(SoundType.Bgm, 0.1f);
        soundVolume.Add(SoundType.Click, 1f);
        soundVolume.Add(SoundType.Button, 0.5f);
    }
    public void PlaySound(SoundType soundType)
    {
        GameObject sound = new GameObject("sound");

        AudioSource audioSource = sound.AddComponent<AudioSource>();
        audioSource.clip = clips[(int)soundType];
        audioSource.volume = soundVolume[soundType];

        if (soundType == SoundType.Bgm)
        {
            audioSource.loop = true;
        }
        else
        {
            Destroy(sound, audioSource.clip.length);
        }
        audioSource.Play();

    }
}

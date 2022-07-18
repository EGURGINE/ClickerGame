using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Bgm,
    Click,
    Button
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
    private void Awake()
    {
        instance = this;
    }

    public void PlaySound(SoundType soundType)
    {
        GameObject sound = new GameObject("sound");

        AudioSource audioSource = sound.AddComponent<AudioSource>();
        if (soundType == SoundType.Bgm)
        {
            audioSource.loop = true;
        }
        audioSource.clip = clips[(int)soundType];
        audioSource.Play();

        Destroy(sound, audioSource.clip.length);
    }
}

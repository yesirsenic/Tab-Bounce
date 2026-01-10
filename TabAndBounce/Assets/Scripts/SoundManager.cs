using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource sfxSource;

    [Header("SFX Clips")]
    [SerializeField] private AudioClip[] sfxClips;

    private Dictionary<SFXType, AudioClip> sfxDict;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitDictionary();
    }

    void InitDictionary()
    {
        sfxDict = new Dictionary<SFXType, AudioClip>();

        for (int i = 0; i < sfxClips.Length; i++)
        {
            sfxDict[(SFXType)i] = sfxClips[i];
        }
    }

    public void Play(SFXType type, float volume = 1f)
    {
        if (!sfxDict.ContainsKey(type))
        {
            Debug.LogWarning($"SFX ¾øÀ½ : {type}");
            return;
        }

        sfxSource.PlayOneShot(sfxDict[type], volume);
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    Image MusicImage;

    [SerializeField]
    AudioSource audioSource;

    private bool musicOn;

    private void Start()
    {
        musicOn = true;
    }

    public void ButtonClickSound()
    {
        SoundManager.Instance.Play(SFXType.Button);
    }

    public void MusicToggle()
    {
        if(musicOn)
        {
            Color c = MusicImage.color;

            c.a = 0.35f;

            MusicImage.color = c;

            audioSource.volume = 0f;

            musicOn=false;
        }

        else
        {
            Color c = MusicImage.color;

            c.a = 1f;

            MusicImage.color = c;

            audioSource.volume = 1f;

            musicOn = true;
        }
    }
}

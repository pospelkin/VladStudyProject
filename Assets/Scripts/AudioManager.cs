using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip swipe;
    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
        musicSource.loop = true;
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void MuteToggle(bool muted)
    {
        if (muted)
        {
            AudioListener.volume= 0;
        }
        else
        {
            AudioListener.volume= 1;
        }
    }
}

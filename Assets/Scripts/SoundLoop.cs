using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLoop : MonoBehaviour
{
    public AudioSource source;
    public AudioSource clickSource;
    public AudioClip soundStart;
    // Start is called before the first frame update
    void Start()
    {
        source.PlayOneShot(soundStart);
        source.PlayScheduled(AudioSettings.dspTime + soundStart.length);
    }

    public void MuteMusic(bool isMuted)
    {
        if (isMuted)
        {
            source.Stop();
        }
        else
        {
            source.Play();
        }
    }
    public void PlayClickSound()
    {
        clickSource.Play();
    }
}

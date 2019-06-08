using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip hitSound;

    public AudioClip deadSound;

    public AudioClip DingSound;

    public AudioClip WrongSound;

    private AudioSource AudioSource;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    //void Update()
    //{
    //    if (AudioSource.clip == deadSound && AudioSource.time > 1.5)
    //        AudioSource.Stop();
    //}

    public void playHitSound()
    {
        AudioSource.PlayOneShot(hitSound);
    }

    public void playDeadSound()
    {
        AudioSource.clip = deadSound;
        AudioSource.Play();
    }

    public void playDingSound()
    {
        AudioSource.PlayOneShot(DingSound);
    }

    public void playWrongSound()
    {
        AudioSource.PlayOneShot(WrongSound);
    }
}

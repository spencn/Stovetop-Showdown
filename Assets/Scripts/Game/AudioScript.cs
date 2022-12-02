using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] soundEffects;
    private AudioClip clipToPlay;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

     public void hitSound()
    {
        clipToPlay = soundEffects[0];
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }
    public void squirtSound()
    {
        clipToPlay = soundEffects[1];
        audioSource.clip = clipToPlay;
        audioSource.PlayDelayed(0.3f);
        
    }
    public void swingSound()
    {
        clipToPlay = soundEffects[2];
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }

    public void fireSound()
    {
        clipToPlay = soundEffects[3];
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }

    public void peaSound()
    {
        clipToPlay = soundEffects[4];
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }

    public void knifeSound()
    {
        clipToPlay = soundEffects[5];
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }
    public void waterSound()
    {
        clipToPlay = soundEffects[6];
        audioSource.clip = clipToPlay;
        audioSource.PlayDelayed(0.65f);
    }
}

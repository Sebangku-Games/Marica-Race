using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSound;
    public bool onSound = true;

    public AudioClip button;
    public AudioClip greenSfx;
    public AudioClip yellowSfx;
    public AudioClip redSfx;
    public AudioClip roundOverSfx;
    public AudioClip gameOverSfx;
    public AudioClip penaltySfx;
    //public AudioClip countdownSfx;
    

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        audioSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(CheckSound());
    }

    IEnumerator CheckSound()
    {
        yield return new WaitForEndOfFrame();
        if (onSound)
        {
            audioSound.Stop();
        }
    }

    public void soundSettings(bool on){
        if (on)
        {
            audioSound.Play();
        }
        else
        {
            audioSound.Stop();
        }
    }

    public void PlayButton(){
        if (onSound)
            audioSound.PlayOneShot(button);
    }

    public void PlayGreenSfx(){
        if (onSound)
            audioSound.PlayOneShot(greenSfx);
    }

    public void PlayYellowSfx(){
        if (onSound)
            audioSound.PlayOneShot(yellowSfx);
    }

    public void PlayRedSfx(){
        if (onSound)
            audioSound.PlayOneShot(redSfx);
    }

    public void PlayRoundOverSfx(){
        if (onSound)
            audioSound.PlayOneShot(roundOverSfx);
    }

    public void PlayGameOverSfx(){
        if (onSound)
            audioSound.PlayOneShot(gameOverSfx);
    }

    public void PlayPenaltySfx(){
        if (onSound)
            audioSound.PlayOneShot(penaltySfx);
    }

    // public void PlayCountdownSfx(){
    //     if (onSound)
    //         audioSound.PlayOneShot(countdownSfx);
    // }

    
}
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
    public AudioClip boostSfx;
    public AudioClip countdownSfx;

    private bool hasSFXGameOverPlayed = false;
    

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        audioSound = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        hasSFXGameOverPlayed = false;
        //StartCoroutine(CheckSound());
        //audioSound.Play();
    }

    public void ToggleSound()
    {
        onSound = !onSound;
        soundSettings();
        Debug.Log("Sound: " + onSound);
    }

    IEnumerator CheckSound()
    {
        yield return new WaitForEndOfFrame();
        if (onSound)
        {
            audioSound.Stop();
        }
    }

    public void soundSettings()
    {
        if (onSound)
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
        if (hasSFXGameOverPlayed)
            return;

        if (onSound)
            audioSound.PlayOneShot(gameOverSfx);
        
        hasSFXGameOverPlayed = true;
    }

    public void PlayPenaltySfx(){
        if (onSound)
            audioSound.PlayOneShot(penaltySfx);
    }

    public void PlayBoostSfx(){
        if (onSound)
            audioSound.PlayOneShot(boostSfx);
    }

    public void PlayCountdownSfx(){
        if (onSound)
            audioSound.PlayOneShot(countdownSfx);
    }

    
}
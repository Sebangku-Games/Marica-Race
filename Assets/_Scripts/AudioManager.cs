using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSound;
    public AudioSource sfxSound;
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
            Destroy(this.gameObject);
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
        sfxSound.PlayOneShot(button);
    }

    public void PlayGreenSfx(){
        sfxSound.PlayOneShot(greenSfx);
    }

    public void PlayYellowSfx(){
        if (onSound)
            sfxSound.PlayOneShot(yellowSfx);
    }

    public void PlayRedSfx(){
        if (onSound)
            sfxSound.PlayOneShot(redSfx);
    }

    public void PlayRoundOverSfx(){
        if (onSound)
            sfxSound.PlayOneShot(roundOverSfx);
    }

    public void PlayGameOverSfx(){
        if (hasSFXGameOverPlayed)
            return;

        if (onSound)
            sfxSound.PlayOneShot(gameOverSfx);
        
        hasSFXGameOverPlayed = true;
    }

    public void PlayPenaltySfx(){
        if (onSound)
            sfxSound.PlayOneShot(penaltySfx);
    }

    public void PlayBoostSfx(){
        if (onSound)
            sfxSound.PlayOneShot(boostSfx);
    }

    public void PlayCountdownSfx(){
        if (onSound)
            sfxSound.PlayOneShot(countdownSfx);
    }

    
}
using System;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public SoundClip[] musicSound, sfxSound;
    public AudioSource musicSource, sfxSource;
    

    private void Start()
    {
       // PlayMusic("MainMenu"); UnComment To Play MainMenuSound Also Add MainMenuSound In List
    }
   
    public void PlayMusic(string Clipname)
    {
        AudioClip clip= GetMusicClip(Clipname);
        musicSource.clip = clip;
        musicSource.Play();
    }
    public void PlaySFX(string Clipname)
    {
        AudioClip clip = GetSFXClip(Clipname);
        musicSource.PlayOneShot(clip);
    }


   ////////GET CLIP FUNCTIONS////////
    private AudioClip GetMusicClip(string name)
    {
        SoundClip s = Array.Find(musicSound, x => x.clipName == name);
        if (s == null) 
        {
            Debug.LogError("No Music with that name, Check Spelling");
            return null;
        }
        return s.AudioClip; //If console Show Error, Checkout line 12
    }
    private AudioClip GetSFXClip(string name)
    {
        SoundClip s = Array.Find(sfxSound, x => x.clipName == name);
        if (s == null)
        {
            Debug.LogError("No SFX with that name, Check Spelling");
            return null;
        }
        return s.AudioClip;
    }
}

using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioMasterLogic : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioMasterLogic instance;
    bool soundActive=true;
    bool themeActive = true;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source= gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Update is called once per frame
    void Start()
    {
        PlaySound("BackgroundMusic");
    }
    public void PlaySound(string name) {
       Sound s= Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
    public void ToggleSound()
    {
        soundActive = !soundActive;
        if (soundActive) { 
            foreach (Sound s in sounds)
            {
                if(s.name!= "BackgroundMusic" || (s.name == "BackgroundMusic" && themeActive)) 
                { 
                s.source.volume = s.volume;
                }
            }
        }
        else
        {
            foreach (Sound s in sounds)
            {
                s.source.volume = 0;
            }
        }
    }

    public void ToggleTheme()
    {
        themeActive = !themeActive;
        if (themeActive && soundActive)
        {
                sounds[1].source.volume = sounds[1].volume;
        }
        else
        {
                sounds[1].source.volume = 0;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    
    public static AudioManager instance;
    
    private Dictionary<SoundState, List<Sound>> DicoActualSound = new Dictionary<SoundState, List<Sound>>();
    
    private void Start()
    {
        foreach(Sound sound in sounds)
        {
            if (!DicoActualSound.ContainsKey(sound.ActualSound))
            {
                DicoActualSound.Add(sound.ActualSound, new List<Sound>());
            }
            DicoActualSound[sound.ActualSound].Add(sound);
        }


    }
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
       foreach (Sound s in sounds)
       {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioMixer;
       }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found (surement mal ecrit entre le script et sur Unity)");
            return;
        }
        s.source.Play();
    }


    public void Stop(SoundState soundState)
    {
        if (DicoActualSound.ContainsKey(soundState))
        {
            int i = Random.Range(0, DicoActualSound[soundState].Count);

            Sound s = DicoActualSound[soundState][i];
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found (surement mal ecrit entre le script et sur Unity)");
                return;
            }
            s.source.Stop();

        }
        else
        {
            Debug.LogWarning("PB de son");
        }
    }
    public void PlayRandom(SoundState soundState)
    {
        if (DicoActualSound.ContainsKey(soundState))
        {
            int i = Random.Range(0, DicoActualSound[soundState].Count);

            Sound s = DicoActualSound[soundState][i];
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found (surement mal ecrit entre le script et sur Unity)");
                return;
            }
            s.source.Play();
        }
        else
        {
            Debug.LogWarning("PB de son");
        }   
    }
}

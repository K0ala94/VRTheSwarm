﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public Sound[] sounds;

	void Start () {
		foreach(Sound sound in sounds)
        {
            if (!sound.localSound)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
                sound.source.clip = sound.clip;
            }
        }

        playSound("forestSound");
	}
	
	public void playSound(string name)
    {
        Sound sound =Array.Find(sounds, s => s.name.Equals(name));
        if(sound != null)
        {
            sound.source.Play();
        }
    }

    public void playSoundWithDelay(string name,float secs)
    {
        Sound sound = Array.Find(sounds, s => s.name.Equals(name));
        if (sound != null)
        {
            ulong delay = (ulong)(secs * 44100);
            sound.source.Play( delay);
        }
    }

    public void stopSound(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name.Equals(name));
        if (sound != null)
        {
            sound.source.Pause();
        }
    }

    public bool isPlaying(String name)
    {
        Sound sound = Array.Find(sounds, s => s.name.Equals(name));
        if (sound != null)
        {
            return sound.source.isPlaying;
        }
        return true;
    }

    public void setVolume(String name, float vol)
    {
        Sound sound = Array.Find(sounds, s => s.name.Equals(name));
        if (sound != null)
        {
            sound.source.volume = vol;
        }
        
    }

    public void playSoundOnObject(String name, GameObject o)
    {
        Sound sound = Array.Find(sounds, s => s.name.Equals(name));
        if (sound != null)
        {
            AudioSource attachedSource = o.AddComponent<AudioSource>();
            sound.source = attachedSource;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.clip = sound.clip;
            sound.source.spatialBlend = 1.0f;

            sound.source.Play();
        }   
    }
}

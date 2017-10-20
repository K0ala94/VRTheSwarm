using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public Sound[] sounds;

	void Start () {
		foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.clip = sound.clip;
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
}

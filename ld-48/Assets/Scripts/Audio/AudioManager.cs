using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    private void Awake() {
        if (instance != null) {
            Destroy(this);
        }

        instance = this;

        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void play(string soundName) {
        Sound s = Array.Find(sounds, sound => sound.soundName == soundName);
        if (s != null) {
            s.source.Play();
        }
    }
}

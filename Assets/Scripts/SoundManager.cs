using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] BGM;
    public AudioClip[] SFX;

    AudioSource _bgmSource;
    AudioSource _sfxSource;
    Dictionary<string, AudioClip> _bgm;
    Dictionary<string, AudioClip> _sfx;

    void Awake()
    {
        _bgmSource = gameObject.AddComponent<AudioSource>();
        _sfxSource = gameObject.AddComponent<AudioSource>();

        _bgmSource.loop = true;

        _bgm = new Dictionary<string, AudioClip>();
        _sfx = new Dictionary<string, AudioClip>();

        foreach(AudioClip a in BGM) {
            _bgm.Add(a.name, a);
        }

        foreach(AudioClip a in SFX) {
            _sfx.Add(a.name, a);
        }
    }

    public void PlayBGM(string name) {
        _bgmSource.clip = _bgm[name];
        _bgmSource.Play();
    }

    public void PlaySFX(string name) {
        _sfxSource.PlayOneShot(_sfx[name]);
    }

    public void SetBGMVolume(float value) {

    }

    public void SetSFXVolume(float value) {
        
    }
}

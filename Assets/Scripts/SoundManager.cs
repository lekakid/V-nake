using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public string BGMPath = "Sound/BGM";
    public string SFXPath = "Sound/SFX";

    public static SoundManager Instance {
        get; private set;
    }

    public float BGMVolume {
        get { return _bgmSource.volume; }
        set { _bgmSource.volume = value; }
    }

    public float SFXVolume {
        get { return _sfxSource.volume; }
        set { _sfxSource.volume = value; }
    }

    AudioSource _bgmSource;
    AudioSource _sfxSource;
    Dictionary<string, AudioClip> _bgm;
    Dictionary<string, AudioClip> _sfx;

    void Awake()
    {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            DestroyImmediate(this.gameObject);
        }

        _bgmSource = gameObject.AddComponent<AudioSource>();
        _sfxSource = gameObject.AddComponent<AudioSource>();

        _bgmSource.loop = true;

        _bgm = new Dictionary<string, AudioClip>();
        _sfx = new Dictionary<string, AudioClip>();

        AudioClip[] bgmList = Resources.LoadAll<AudioClip>(BGMPath);
        AudioClip[] sfxList = Resources.LoadAll<AudioClip>(SFXPath);

        foreach(AudioClip a in bgmList) {
            _bgm.Add(a.name, a);
        }

        foreach(AudioClip a in sfxList) {
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
}

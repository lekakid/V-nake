using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundManager : MonoBehaviour
{
    public string BGMPath = "Sound/BGM";
    public string SFXPath = "Sound/SFX";

    public static SoundManager Instance { get; private set;}

    public float MasterVolume { get; set; }
    public float BGMVolume { get; set; }
    public float SFXVolume { get; set; }

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
        else if(Instance != null & Instance != this) {
            DestroyImmediate(this.gameObject);
            return;
        }

        _bgmSource = gameObject.AddComponent<AudioSource>();
        _sfxSource = gameObject.AddComponent<AudioSource>();

        _bgmSource.loop = true;

        // Load saved volume options

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
        if(_bgmSource.clip != null) {
            _bgmSource.DOFade(0, 0.6f);
        }
        _bgmSource.clip = _bgm[name];
        _bgmSource.volume = MasterVolume * BGMVolume * 0.5f;
        _bgmSource.Play();
        _bgmSource.DOFade(MasterVolume * BGMVolume, 0.6f);
    }

    public void PlaySFX(string name) {
        _sfxSource.PlayOneShot(_sfx[name]);
    }
}

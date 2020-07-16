using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundManager : MonoBehaviour
{
    public string BGMPath = "Sound/BGM";
    public string SFXPath = "Sound/SFX";

    public static SoundManager Instance { get; private set;}

    private float _masterVolume;
    public float MasterVolume { 
        get { return _masterVolume; }
        set {
            _masterVolume = value;
            _bgmSource.volume = _bgmVolume * _masterVolume;
            _sfxSource.volume = _sfxVolume * _masterVolume;
        }
    }
    private float _bgmVolume;
    public float BGMVolume { 
        get { return _bgmVolume; }
        set {
            _bgmVolume = value;
            _bgmSource.volume = _bgmVolume * _masterVolume;
        }
    }
    private float _sfxVolume;
    public float SFXVolume { 
        get { return _sfxVolume; }
        set {
            _sfxVolume = value;
            _sfxSource.volume = _sfxVolume * _masterVolume;
        }
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
        else if(Instance != null & Instance != this) {
            DestroyImmediate(this.gameObject);
            return;
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

    void Start() {
        MasterVolume = PlayerPrefs.GetFloat("Volume.Master", 1f);
        BGMVolume = PlayerPrefs.GetFloat("Volume.BGM", 1f);
        SFXVolume = PlayerPrefs.GetFloat("Volume.SFX", 1f);
    }

    void OnApplicationQuit() {
        PlayerPrefs.SetFloat("Volume.Master", MasterVolume);
        PlayerPrefs.SetFloat("Volume.BGM", BGMVolume);
        PlayerPrefs.SetFloat("Volume.SFX", SFXVolume);

        Instance = null;
    }

    public void PlayBGM(string name) {
        float time = 0;

        if(_bgmSource.clip != null) {
            time = 0.6f;
        }

        _bgmSource.DOFade(0f, time).SetUpdate(true).OnComplete(() => {
            _bgmSource.clip = _bgm[name];
            _bgmSource.volume = _bgmVolume * _masterVolume;
            _bgmSource.Play();
        });
    }

    public void StopBGM() {
        _bgmSource.DOFade(0f, 0.6f).SetUpdate(true).OnComplete(() => {
            _bgmSource.Stop();
            _bgmSource.clip = null;
        });
    }

    public void PlaySFX(string name) {
        _sfxSource.PlayOneShot(_sfx[name]);
    }
}

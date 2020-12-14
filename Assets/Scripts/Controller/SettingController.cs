using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingController : MonoBehaviour
{
    public SettingView SettingView;

    VolumeMixer.MixerType _selected;

    int _master;
    int _bgm;
    int _sfx;

    void Start() {
        _master = (int)(SoundManager.Instance.MasterVolume * 20f) * 5;
        _bgm = (int)(SoundManager.Instance.BGMVolume * 20f) * 5;
        _sfx = (int)(SoundManager.Instance.SFXVolume * 20f) * 5;

        SettingView.SetValue(_master, _bgm, _sfx);
    }

    void Update() {
        if(Input.GetButtonDown("Cancel")) {
            Back();
            return;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        bool xdown = Input.GetButtonDown("Horizontal");
        bool ydown = Input.GetButtonDown("Vertical");

        if(xdown) {
            if(x > 0f)
                SelectNextMixer();
            else
                SelectPrevMixer();
        }

        if(ydown) {
            if(y > 0f)
                UpVolume(_selected);
            else
                DownVolume(_selected);

            SettingView.SetValue(_master, _bgm, _sfx);
        }
    }

    void OnEnable() {
        _selected = VolumeMixer.MixerType.MASTER;
        SettingView.SetSelectColor(_selected);
        SettingView.Show();
    }

    void OnDisable() {
        if(SettingView)
            SettingView.Hide();
    }

    public void SelectNextMixer() {
        switch(_selected) {
            case VolumeMixer.MixerType.MASTER:
                _selected = VolumeMixer.MixerType.BGM;
                break;
            case VolumeMixer.MixerType.BGM:
                _selected = VolumeMixer.MixerType.SFX;
                break;
        }

        SettingView.SetSelectColor(_selected);
    }

    public void SelectPrevMixer() {
        switch(_selected) {
            case VolumeMixer.MixerType.BGM:
                _selected = VolumeMixer.MixerType.MASTER;
                break;
            case VolumeMixer.MixerType.SFX:
                _selected = VolumeMixer.MixerType.BGM;
                break;
        }

        SettingView.SetSelectColor(_selected);
    }

    public void UpVolume(int type) {
        _selected = (VolumeMixer.MixerType)type;
        UpVolume(_selected);
    }

    public void DownVolume(int type) {
        _selected = (VolumeMixer.MixerType)type;
        DownVolume(_selected);
    }

    public void UpVolume(VolumeMixer.MixerType type) {
        switch(type) {
            case VolumeMixer.MixerType.MASTER:
                _master += (_master < 100) ? 5 : 0;
                break;
            case VolumeMixer.MixerType.BGM:
                _bgm += (_bgm < 100) ? 5 : 0;
                break;
            case VolumeMixer.MixerType.SFX:
                _sfx += (_sfx < 100) ? 5 : 0;
                break;
        }

        SettingView.SetValue(_master, _bgm, _sfx);
        SettingView.SetSelectColor(type);
        ApplyVolume();
    }

    public void DownVolume(VolumeMixer.MixerType type) {
        switch(type) {
            case VolumeMixer.MixerType.MASTER:
                _master -= (_master > 0) ? 5 : 0;
                break;
            case VolumeMixer.MixerType.BGM:
                _bgm -= (_bgm > 0) ? 5 : 0;
                break;
            case VolumeMixer.MixerType.SFX:
                _sfx -= (_sfx > 0) ? 5 : 0;
                break;
        }

        SettingView.SetValue(_master, _bgm, _sfx);
        SettingView.SetSelectColor(type);
        ApplyVolume();
    }

    void ApplyVolume() {
        SoundManager.Instance.MasterVolume = _master / 100f;
        SoundManager.Instance.BGMVolume = _bgm / 100f;
        SoundManager.Instance.SFXVolume = _sfx / 100f;

        PlayerPrefs.SetFloat("Volume.Master", _master / 100f);
        PlayerPrefs.SetFloat("Volume.BGM", _bgm / 100f);
        PlayerPrefs.SetFloat("Volume.SFX", _sfx / 100f);
    }

    public void Back() {
        GameManager.UndoController();
    }
}

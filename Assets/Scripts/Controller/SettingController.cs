using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingController : MonoBehaviour
{
    public AudioMixer MainMixer;
    public SettingView SettingView;

    string _selected;

    Dictionary<string, float> _volumes;
    Queue<string> _typeQueue;

    void Start() {
        float master = PlayerPrefs.GetFloat("Setting.Master", 0.5f);
        float bgm = PlayerPrefs.GetFloat("Setting.BGM", 1f);
        float sfx = PlayerPrefs.GetFloat("Setting.SFX", 1f);

        _volumes = new Dictionary<string, float>();

        _volumes.Add("Master", master);
        _volumes.Add("BGM", bgm);
        _volumes.Add("SFX", sfx);

        SettingView.SetValue(_volumes);
    }

    void Update() {
        if(Input.GetButtonDown("Cancel")) {
            Back();
            return;
        }
    }

    void OnEnable() {
        SettingView.Show();
    }

    void OnDisable() {
        if(SettingView)
            SettingView.Hide();
    }

    public void SetType(string type) {
        _selected = type;
    }

    public void SetVolume(float value) {
        _volumes[_selected] = value;
        MainMixer.SetFloat(_selected, Mathf.Log10(value) * 20f);
        PlayerPrefs.SetFloat($"Setting.{_selected}", value);
    }

    public void Back() {
        GameManager.UndoController();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public MenuSelectorView TitleView;
    public GameObject HowtoView;
    public SettingView SettingView;

    public TitleManager Instance { get; private set; }
    public enum ViewStateType { TITLE, HOWTO, SETTING }
    public ViewStateType ViewState { get; private set; }

    int _selected;

    void Start() {
        SoundManager.Instance.PlayBGM("Title");
    }

    void Update() {
        switch(ViewState) {
            case ViewStateType.TITLE:
                OnTitleMenu();
                break;
            case ViewStateType.HOWTO:
                if(Input.GetButtonDown("Submit") || Input.GetMouseButtonDown(0)) {
                    HowtoView.SetActive(false);
                    ViewState = ViewStateType.TITLE;
                }
                break;
            case ViewStateType.SETTING:
                OnSettingMenu();
                break;
        }
    }

    void OnTitleMenu() {
        float y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("Vertical")) {
            if(y > 0f)
                TitleView.SelectPrev();
            else
                TitleView.SelectNext();
        }

        if(Input.GetButtonDown("Submit")) {
            switch(TitleView.selected) {
                case 0:
                    Play();
                    break;
                case 1:
                    Howto();
                    break;
                case 2:
                    Gallery();
                    break;
                case 3:
                    ShowSetting();
                    break;
                case 4:
                    Quit();
                    break;
            }
        }
    }

    void OnSettingMenu() {
        if(Input.GetButtonDown("Cancel")) {
            HideSetting();
            return;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        bool xdown = Input.GetButtonDown("Horizontal");
        bool ydown = Input.GetButtonDown("Vertical");

        if(xdown) {
            if(x > 0f)
                SettingView.SelectVolumeNext();
            else
                SettingView.SelectVolumePrev();
        }

        if(ydown) {
            if(y > 0f)
                SettingView.UpVolume();
            else
                SettingView.DownVolume();
        }
    }

    public void Play() {
        SoundManager.Instance.PlaySFX("Select");
        SceneManager.LoadScene("Snake");
    }

    public void Howto() {
        SoundManager.Instance.PlaySFX("Select");
        ViewState = ViewStateType.HOWTO;
        HowtoView.SetActive(true);
    }

    public void Gallery() {
        SoundManager.Instance.PlaySFX("Select");
    }

    public void ShowSetting() {
        SoundManager.Instance.PlaySFX("Select");
        ViewState = ViewStateType.SETTING;
        SettingView.SetActive(true);
    }

    public void HideSetting() {
        SoundManager.Instance.PlaySFX("Select");
        ViewState = ViewStateType.TITLE;
        SettingView.SetActive(false);
    }

    public void Quit() {
        SoundManager.Instance.PlaySFX("Select");
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("View")]
    public PlayView PlayView;
    public MenuSelectorView PauseView;
    public SettingView SettingView;
    public ResultView ResultView;

    [Header("Control")]
    public SnakeController SnakeController;
    public SpawnController SpawnController;

    public static GameManager Instance { get; private set; }
    public enum ViewStateType { PLAY, PAUSEMENU, SETTING, GAMEOVER }
    public ViewStateType ViewState { get; private set; }

    void Awake() {
        Instance = this;
    }

    void Start() {
        SoundManager.Instance.PlayBGM("Snake");
        ViewState = ViewStateType.PLAY;
    }

    void OnDestroy() {
        Instance = null;
    }

    void Update() {
        switch(ViewState) {
            case ViewStateType.PLAY:
                OnPlay();
                break;
            case ViewStateType.PAUSEMENU:
                OnPauseMenu();
                break;
            case ViewStateType.SETTING:
                OnSettingMenu();
                break;
            case ViewStateType.GAMEOVER:
                break;
        }
    }

    void OnPlay() {
        if(Input.GetButtonDown("Cancel")) {
            Pause();
            return;
        }
    }

    void OnPauseMenu() {
        if(Input.GetButtonDown("Cancel")) {
            Resume();
            return;
        }

        float y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("Vertical")) {
            if(y > 0)
                PauseView.SelectPrev();
            else if(y < 0)
                PauseView.SelectNext();
        }

        if(Input.GetButtonDown("Submit")) {
            switch(PauseView.selected) {
                case 0:
                    Resume();
                    break;
                case 1:
                    Restart();
                    break;
                case 2:
                    ShowSetting();
                    break;
                case 3:
                    ReturnTitle();
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
            if(x > 0)
                SettingView.SelectVolumeNext();
            else
                SettingView.SelectVolumePrev();
        }

        if(ydown) {
            if(y > 0)
                SettingView.UpVolume();
            else
                SettingView.DownVolume();
        }
    }

    void Init() {
        PlayView.Init();
        PauseView.SetActive(false);
        ResultView.Init();
        SnakeController.Init();
        SpawnController.Init();
    }

    public void Pause() {
        Time.timeScale = 0;
        PauseView.SetActive(true);
        GameManager.Instance.ViewState = ViewStateType.PAUSEMENU;
    }

    public void Resume() {
        Time.timeScale = 1;
        PauseView.SetActive(false);
        GameManager.Instance.ViewState = ViewStateType.PLAY;
    }

    public void Restart() {
        Init();
        Time.timeScale = 1;
        ResultView.SetActive(false);
        GameManager.Instance.ViewState = ViewStateType.PLAY;
    }

    public void GameOver() {
        Time.timeScale = 0;
        ResultView.SetActive(true);
        GameManager.Instance.ViewState = ViewStateType.GAMEOVER;
    }

    public void ShowSetting() {
        SettingView.SetActive(true);
        GameManager.Instance.ViewState = ViewStateType.SETTING;
    }

    public void HideSetting() {
        SettingView.SetActive(false);
        GameManager.Instance.ViewState = ViewStateType.PAUSEMENU;
    }

    public void ReturnTitle() {
        SnakeController.Init();
        SceneManager.LoadScene("Title");
    }

    public void Quit() {
        Application.Quit();
    }
}

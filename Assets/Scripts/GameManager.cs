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
    public MenuSelectorView ResultSelectorView;

    [Header("Controller")]
    public SnakeController SnakeController;

    public static GameManager Instance { get; private set; }
    public enum ViewStateType { PLAY, PAUSEMENU, SETTING, GAMEOVER, ENDING, TRUEEND }
    public ViewStateType ViewState { get; private set; }

    void Awake() {
        Instance = this;
    }

    void Start() {
        SoundManager.Instance.PlayBGM("Snake");
        Time.timeScale = 1f;
        Application.targetFrameRate = 60;
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
                OnGameOverMenu();
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
            Play();
            return;
        }

        float y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("Vertical")) {
            if(y > 0f)
                PauseView.SelectPrev();
            else if(y < 0f)
                PauseView.SelectNext();
        }

        if(Input.GetButtonDown("Submit")) {
            switch(PauseView.selected) {
                case 0:
                    Play();
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

    void OnGameOverMenu() {
        float y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("Vertical")) {
            if(y > 0f)
                ResultSelectorView.SelectPrev();
            else if(y < 0f)
                ResultSelectorView.SelectNext();
        }

        if(Input.GetButtonDown("Submit")) {
            switch(ResultSelectorView.selected) {
                case 0:
                    Restart();
                    break;
                case 1:
                    ReturnTitle();
                    break;
            }
        }
    }

    void Init() {
        SnakeController.Init();
    }

    public void Pause() {
        SoundManager.Instance.PlaySFX("Select");
        Time.timeScale = 0f;
        PauseView.SetActive(true);
        GameManager.Instance.ViewState = ViewStateType.PAUSEMENU;
    }

    public void Play() {
        SoundManager.Instance.PlaySFX("Select");
        Time.timeScale = 1f;
        PauseView.SetActive(false);
        GameManager.Instance.ViewState = ViewStateType.PLAY;
    }

    public void Restart() {
        SoundManager.Instance.PlaySFX("Select");
        SoundManager.Instance.PlayBGM("Snake");
        Init();
        SnakeController.Play();
        Time.timeScale = 1f;
        PauseView.SetActive(false);
        ResultView.SetActive(false);
        GameManager.Instance.ViewState = ViewStateType.PLAY;
    }

    public void GameOver() {
        Time.timeScale = 0f;
        ResultView.SetActive(true);
        SnakeController.CharacterDatabase.UpdateScore();
        GameManager.Instance.ViewState = ViewStateType.GAMEOVER;
    }

    public void ShowSetting() {
        SoundManager.Instance.PlaySFX("Select");
        SettingView.SetActive(true);
        GameManager.Instance.ViewState = ViewStateType.SETTING;
    }

    public void HideSetting() {
        SoundManager.Instance.PlaySFX("Select");
        SettingView.SetActive(false);
        GameManager.Instance.ViewState = ViewStateType.PAUSEMENU;
    }

    public void ReturnTitle() {
        SoundManager.Instance.PlaySFX("Select");
        SnakeController.Init();
        SceneManager.LoadScene("Title");
    }

    public void Quit() {
        SoundManager.Instance.PlaySFX("Select");
        Application.Quit();
    }
}

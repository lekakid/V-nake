using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeManager : MonoBehaviour
{
    [Header("View")]
    public PlayView PlayView;
    public PauseView PauseView;
    public ResultView ResultView;

    [Header("Control")]
    public SnakeController SnakeController;
    public SpawnController SpawnController;

    void Start()
    {
        GameManager.Instance.CurrentManager = this.gameObject;
        GameManager.Instance.OnStateChangedBefore += OnStateChangedBefore;
        GameManager.Instance.OnStateChangedAfter += OnStateChangedAfter;

        SoundManager.Instance.PlayBGM("Snake");
        GameManager.Instance.State = GameStateType.PLAY;
    }

    void OnDestroy() {
        GameManager.Instance.CurrentManager = null;
        GameManager.Instance.OnStateChangedBefore -= OnStateChangedBefore;
        GameManager.Instance.OnStateChangedAfter -= OnStateChangedAfter;
    }

    public void OnStateChangedBefore() {
        switch(GameManager.Instance.State) {
            case GameStateType.PLAY:
                Time.timeScale = 0;
                break;
            case GameStateType.GAMEOVER:
                Init();
                break;
            case GameStateType.PAUSEMENU:
                PauseView.gameObject.SetActive(false);
                break;
        }
    }

    public void OnStateChangedAfter() {
        switch(GameManager.Instance.State) {
            case GameStateType.PLAY:
                Time.timeScale = 1;
                break;
            case GameStateType.PAUSEMENU:
                PauseView.gameObject.SetActive(true);
                break;
        }
    }

    void Init() {
        PlayView.Init();
        PauseView.gameObject.SetActive(false);
        ResultView.Init();
        SnakeController.Init();
        SpawnController.Init();
    }

    public void Resume() {
        GameManager.Instance.State = GameStateType.PLAY;
    }

    public void Restart() {
        Init();
        GameManager.Instance.State = GameStateType.PLAY;
    }

    public void ReturnTitle() {
        SnakeController.Init();
        SceneManager.LoadScene("Title");
    }

    public void Quit() {
        Application.Quit();
    }
}

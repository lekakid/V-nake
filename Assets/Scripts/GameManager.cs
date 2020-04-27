using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStateType { TITLE, PLAY, PAUSE, GAMEOVER }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameStateType State { get; private set; }

    public SnakeManager SnakeManager { get; set; }
    public TitleManager TitleManager { get; set; }

    void Awake()
    {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(Instance != null && Instance != this) {
            DestroyImmediate(this.gameObject);
            return;
        }
    }

    public void LoadTitle() {
        SceneManager.LoadScene("Title");
        Time.timeScale = 1;
        State = GameStateType.TITLE;
    }

    public void LoadSnake() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Snake");
        State = GameStateType.PLAY;
    }

    public void Pause() {
        Time.timeScale = 0;
        State = GameStateType.PAUSE;
    }

    public void Resume() {
        Time.timeScale = 1;
        State = GameStateType.PLAY;
    }

    public void GameOver() {
        Time.timeScale = 0;
        State = GameStateType.GAMEOVER;
    }
}

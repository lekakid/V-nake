using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStateType { TITLE, PLAY, PAUSE, GAMEOVER }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameStateType State { get; private set; }

    public PlayView PlayView { get; private set; }
    public PauseView PauseView { get; private set; }
    public ResultView ResultView { get; private set; }
    public SnakeController SnakeController { get; private set; }
    public SpawnManager SpawnManager { get; private set; }

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

        SceneManager.sceneLoaded += LoadComponents;
        SceneManager.sceneUnloaded += UnloadComponents;
    }

    void LoadComponents(Scene scene, LoadSceneMode mode) {
        if(scene.name == "Snake") {
            Canvas canvas = FindObjectOfType<Canvas>();
            PlayView = canvas.GetComponentInChildren<PlayView>(true);
            PauseView = canvas.GetComponentInChildren<PauseView>(true);
            ResultView = canvas.GetComponentInChildren<ResultView>(true);
            SnakeController = FindObjectOfType<SnakeController>();
            SpawnManager = FindObjectOfType<SpawnManager>();
        }
    }

    void UnloadComponents(Scene scene) {
        if(scene.name == "Snake") {
            PlayView = null;
            PauseView = null;
            ResultView = null;
            SnakeController = null;
            SpawnManager = null;
        }
    }

    public void Play() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Snake");
        State = GameStateType.PLAY;
    }

    public void Pause() {
        PauseView.Show();
        Time.timeScale = 0;
        State = GameStateType.PAUSE;
    }

    public void Resume() {
        PauseView.Hide();
        Time.timeScale = 1;
        State = GameStateType.PLAY;
    }

    public void Restart() {
        SpawnManager.Init();
        SnakeController.Init();
        PlayView.Init();
        PauseView.Hide();
        ResultView.Init();
        Time.timeScale = 1;
        State = GameStateType.PLAY;
    }

    public void ReturnMenu() {
        SnakeController.Init();
        SceneManager.LoadScene("Title");
        Time.timeScale = 1;
        State = GameStateType.TITLE;
    }

    public void GameOver() {
        ResultView.Show();
        Time.timeScale = 0;
        State = GameStateType.GAMEOVER;
    }
}

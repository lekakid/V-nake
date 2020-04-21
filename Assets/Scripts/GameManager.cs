using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStateType { PLAY, PAUSE, GAMEOVER }

public class GameManager : MonoBehaviour
{
    [Header("Object")]
    public SnakeController SnakeController;

    [Header("View")]
    public GameObject PauseView;
    public ResultView ResultView;

    public static GameManager Instance { get; private set; }
    public GameStateType State { get; private set; }

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        Play();
    }

    void Update()
    {
        switch(State) {
            case GameStateType.PAUSE:
            case GameStateType.GAMEOVER:
                Time.timeScale = 0;
                break;
            case GameStateType.PLAY:
                Time.timeScale = 1;
                break;
        }
    }

    public void Play() {
        SnakeController.Play();
        State = GameStateType.PLAY;
    }

    public void Pause() {
        PauseView.SetActive(true);
        State = GameStateType.PAUSE;
    }

    public void Resume() {
        PauseView.SetActive(false);
        State = GameStateType.PLAY;
    }

    public void Restart() {
        SnakeController.Init();
        SpawnManager.Instance.Init();
        PauseView.SetActive(false);
        ResultView.gameObject.SetActive(false);
        SnakeController.Play();

        State = GameStateType.PLAY;
    }

    public void ReturnMenu() {
        //DestoryImmediate(this.gameObject);
        //EditorSceneManager.LoadScene("Mainmenu");
    }

    public void GameOver() {
        ResultView.gameObject.SetActive(true);
        ResultView.Show();

        State = GameStateType.GAMEOVER;
    }
}

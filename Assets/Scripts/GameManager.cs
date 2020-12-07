using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public Stack<MonoBehaviour> ControllerStack;

    public static GameManager Instance { get; private set; }

    void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            ControllerStack = new Stack<MonoBehaviour>();
        }
        else {
            DestroyImmediate(gameObject);
        }
    }

    void Start() {
        InitScene(SceneManager.GetActiveScene().name);
    }

    void OnApplicationQuit() {
        Instance = null;
    }

    public static void LoadScene(string name) {
        DOTween.KillAll();
        SceneManager.LoadScene(name);
        InitScene(name);
    }

    public static void InitScene(string name) {
        switch(name) {
            case "Snake":
                Application.targetFrameRate = 60;
                SoundManager.Instance.PlayBGM("Snake");
                break;            
            case "Gallery":
                Application.targetFrameRate = 60;
                break;
            case "Title":
                Application.targetFrameRate = 24;
                SoundManager.Instance.PlayBGM("Title");
                break;
        }

        Time.timeScale = 1f;
    }

    public static void Pause() {
        Time.timeScale = 0f;
    }

    public static void Resume() {
        Time.timeScale = 1;
    }

    public static void PushController(MonoBehaviour controller) {
        Instance.ControllerStack.Push(controller);
        controller.enabled = false;
    }

    public static void PopController() {
        if(Instance.ControllerStack.Count == 0)
            return;

        Instance.ControllerStack.Pop().enabled = true;
    }
}

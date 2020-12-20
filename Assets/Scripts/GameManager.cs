using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    Stack<MonoBehaviour> ControllerStack = new Stack<MonoBehaviour>();
    MonoBehaviour CurrentController = null;

    public static GameManager Instance { get; private set; }

    void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
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
            case "Gallery":
                Application.targetFrameRate = 60;
                break;
            case "Title":
                Application.targetFrameRate = 24;
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

    public static void SetController(MonoBehaviour controller) {
        if(Instance.CurrentController != null) {
            Instance.CurrentController.enabled = false;
            Instance.ControllerStack.Push(Instance.CurrentController);

        }
        Instance.CurrentController = controller;
        controller.enabled = true;
    }

    public static void UndoController() {
        Instance.CurrentController.enabled = false;
        Instance.CurrentController = Instance.ControllerStack.Pop();
        Instance.CurrentController.enabled = true;
    }
}

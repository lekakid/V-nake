using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public AudioMixer MainMixer;

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
        float master = PlayerPrefs.GetFloat("Setting.Master", 0.5f);
        float bgm = PlayerPrefs.GetFloat("Setting.BGM", 1f);
        float sfx = PlayerPrefs.GetFloat("Setting.SFX", 1f);

        MainMixer.SetFloat("Master", Mathf.Log10(master) * 20f);
        MainMixer.SetFloat("BGM", Mathf.Log10(bgm) * 20f);
        MainMixer.SetFloat("SFX", Mathf.Log10(sfx) * 20f);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStateType { TITLE, HOWTO, PLAY, PAUSEMENU, SETTING, GAMEOVER }

public delegate void HandleStateChangedBefore();
public delegate void HandleStateChangedAfter();

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject CurrentManager { get; set; }
    private GameStateType _state;
    public GameStateType State { 
        get {
            return _state;
        }
        set {
            if(OnStateChangedBefore != null)
                OnStateChangedBefore();
            _state = value;
            if(OnStateChangedAfter != null)
                OnStateChangedAfter();
        }
    }

    public event HandleStateChangedBefore OnStateChangedBefore;
    public event HandleStateChangedAfter OnStateChangedAfter;

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

    void OnApplicationQuit() {
        Instance = null;
    }
}

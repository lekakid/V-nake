using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
        GameManager.Instance.SnakeManager = this;
        //SoundManager.Instance.PlayBGM("Snake");
        Play();
    }

    public void Init() {
        PlayView.Init();
        PauseView.Hide();
        ResultView.Init();
        SnakeController.Init();
        SpawnController.Init();
    }

    public void Play() {
        SnakeController.Play();
    }

    public void Pause() {
        SoundManager.Instance.PlaySFX("Select");
        GameManager.Instance.Pause();
    }

    public void Resume() {
        SoundManager.Instance.PlaySFX("Select");
        GameManager.Instance.Resume();
    }

    public void Restart() {
        SoundManager.Instance.PlaySFX("Select");
        Init();
        Play();
    }

    public void ReturnTitle() {
        SoundManager.Instance.PlaySFX("Select");
        GameManager.Instance.LoadTitle();
    }
}

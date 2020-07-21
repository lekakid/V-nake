using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public SettingView SettingView;
    MenuView PauseView;

    void Awake() {
        GameManager.PauseController = this;
        PauseView = GetComponent<MenuView>();
    }

    void Update() {
        if(UIManager.Instance.Current != PauseView)
            return;

        if(Input.GetButtonDown("Cancel")) {
            UIManager.Instance.Pop();
            GameManager.Resume();
            return;
        }

        float y = Input.GetAxisRaw("Vertical");
        bool ydown = Input.GetButtonDown("Vertical");

        if(ydown) {
            if(y > 0f)
                PauseView.SelectPrev();
            else
                PauseView.SelectNext();
        }

        if(Input.GetButtonDown("Submit")) {
            switch(PauseView.SelectorValue) {
                case 0:
                    Resume();
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

    public void Resume() {
        UIManager.Instance.Pop();
        GameManager.Resume();
    }

    public void Restart() {
        GameManager.SnakeController.Reset();
        SoundManager.Instance.PlayBGM("Snake");
        UIManager.Instance.Pop();
        GameManager.Resume();
    }

    public void ShowSetting() {
        UIManager.Instance.Push(SettingView);
    }

    public void ReturnTitle() {
        GameManager.LoadScene("Title");
    }

    public void Quit() {
        Application.Quit();
    }
}

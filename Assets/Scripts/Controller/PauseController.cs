using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public MenuView PauseView;
    public SnakeController SnakeController;
    public SettingController SettingController;

    void Update() {
        if(Input.GetButtonDown("Cancel")) {
            Resume();
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

    void OnEnable() {
        PauseView.Show();
    }

    void OnDisable() {
        if(PauseView != null)
            PauseView.Hide();
    }

    public void Resume() {
        this.enabled = false;
        GameManager.UndoController();
        GameManager.Resume();
    }

    public void Restart() {
        SnakeController.Reset();
        Resume();
    }

    public void ShowSetting() {
        GameManager.SetController(SettingController);
    }

    public void ReturnTitle() {
        GameManager.LoadScene("Title");
    }

    public void Quit() {
        Application.Quit();
    }
}

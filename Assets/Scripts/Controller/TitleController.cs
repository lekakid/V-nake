using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    public SettingController SettingController;
    public PopupController PopupController;

    public MenuView TitleView;
    public Animator Animator;

    void Start() {
        TitleView.Show();
        GameManager.SetController(this);
    }

    void Update() {
        if(TitleView.isAnimating) {
            if(Input.anyKeyDown) {
                Animator.speed = 50f;
                return;
            }
        }

        float y = Input.GetAxisRaw("Vertical");
        bool ydown = Input.GetButtonDown("Vertical");

        if(ydown) {
            if(y > 0f)
                TitleView.SelectPrev();
            else
                TitleView.SelectNext();
        }

        if(Input.GetButtonDown("Submit")) {
            switch(TitleView.SelectorValue) {
                case 0:
                    StartSnake();
                    break;
                case 1:
                    ShowHowTo();
                    break;
                case 2:
                    VisitGallery();
                    break;
                case 3:
                    ShowSetting();
                    break;
                case 4:
                    Quit();
                    break;
            }
        }
    }

    public void StartSnake() {
        GameManager.LoadScene("Snake");
    }

    public void ShowHowTo() {
        GameManager.SetController(PopupController);
    }

    public void VisitGallery() {
        GameManager.LoadScene("Gallery");
    }

    public void ShowSetting() {
        GameManager.SetController(SettingController);
    }

    public void Quit() {
        Application.Quit();
    }
}

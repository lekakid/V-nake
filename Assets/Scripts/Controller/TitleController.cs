using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public SettingController SettingController;
    public PopupController PopupController;

    public MenuView TitleView;
    public Animator Animator;

    public RuntimeAnimatorController SpecialAnimator;

    void Awake() {
        GameManager.SetController(this);
    }

    void Start() {
        if(Status.Instance.Ending)
            Animator.runtimeAnimatorController = SpecialAnimator;
        TitleView.Show();
        SoundManager.Instance.PlayBGM("Title");
    }

    void Update() {
        if(TitleView.isAnimating && Input.anyKeyDown) {
            TitleView.Skip();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    public UIView HowtoView;
    public SettingView SettingView;
    MenuView TitleView;

    Animator _animator;

    void Awake() {
        _animator = GetComponent<Animator>();
        TitleView = GetComponent<MenuView>();
    }

    void Update() {
        if(UIManager.Instance.Current != TitleView)
            return;

        if(TitleView.isAnimating) {
            if(Input.anyKeyDown) {
                _animator.speed = 50f;
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
        UIManager.Instance.Push(HowtoView);
    }

    public void VisitGallery() {
        GameManager.LoadScene("Gallery");
    }

    public void ShowSetting() {
        UIManager.Instance.Push(SettingView);
    }

    public void Quit() {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultController : MonoBehaviour
{
    ResultView ResultView;

    Animator _animator;

    void Awake() {
        GameManager.ResultController = this;

        _animator = GetComponent<Animator>();
        ResultView = GetComponent<ResultView>();
    }

    void Update() {
        if(UIManager.Instance.Current != ResultView)
            return;

        if(ResultView.isAnimating) {
            if(Input.GetButtonDown("Submit")) {
                _animator.speed = 50f;
                return;
            }
        }

        float y = Input.GetAxisRaw("Vertical");
        bool ydown = Input.GetButtonDown("Vertical");

        if(ydown) {
            if(y > 0f)
                ResultView.SelectPrev();
            else
                ResultView.SelectNext();
        }

        if(Input.GetButtonDown("Submit")) {
            switch(ResultView.SelectorValue) {
                case 0:
                    Restart();
                    break;
                case 1:
                    ReturnTitle();
                    break;
            }
        }
    }

    public void Restart() {
        GameManager.SnakeController.Reset();
        SoundManager.Instance.PlayBGM("Snake");
        UIManager.Instance.Pop();
        GameManager.Resume();
    }

    public void ReturnTitle() {
        GameManager.LoadScene("Title");
    }
}

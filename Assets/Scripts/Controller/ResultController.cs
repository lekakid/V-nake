using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultController : MonoBehaviour
{
    public ResultView ResultView;
    public SnakeController SnakeController;
    public CharacterDatabase CharacterDatabase;

    Animator _animator;

    void Awake() {
        _animator = GetComponent<Animator>();
    }

    void OnEnable() {
        ResultView.Show();
    }

    void OnDisable() {
        if(ResultView != null)
            ResultView.Hide();
    }

    void Update() {
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

    public void DrawResult() {
        ResultView.DrawResult();
    }

    public void Restart() {
        SoundManager.Instance.PlayBGM("Snake");
        SnakeController.Reset();
        GameManager.UndoController();
        GameManager.Resume();
    }

    public void ReturnTitle() {
        GameManager.LoadScene("Title");
    }
}

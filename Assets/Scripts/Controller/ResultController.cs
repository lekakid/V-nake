using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultController : MonoBehaviour
{
    public ResultView ResultView;
    public SnakeController SnakeController;
    public DialogueController DialogueController;
    public CharacterDatabase CharacterDatabase;

    Animator _animator;

    void Awake() {
        _animator = GetComponent<Animator>();
    }

    void OnEnable() {
        ResultView.Show();
    }

    void Update() {
        if(ResultView.isAnimating) {
            if(Input.GetButtonDown("Submit")) {
                ResultView.Skip();
            }
            return;
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

    bool CheckEndingShow() {
        if(Status.Instance.Ending)
            return false;

        foreach(string name in Status.Instance.CharacterRescueCounts.Keys) {
            if(Status.Instance.CharacterRescueCounts[name] < 1)
                return false;
        }

        if(Status.Instance.CurrentRescueCount < 80){
            return false;
        }

        return true;
    }

    public void ShowEnding() {
        if(CheckEndingShow()) {
            Status.Instance.Ending = true;
            Status.Instance.Save();
            GameManager.SetController(DialogueController);
            DialogueController.RunDialogueScript("Ending");
        }
    }

    public void DrawResult() {
        ResultView.DrawResult();
    }

    public void Restart() {
        SnakeController.Reset();
        ResultView.Hide();
        GameManager.UndoController();
        GameManager.Resume();
    }

    public void ReturnTitle() {
        GameManager.LoadScene("Title");
    }
}

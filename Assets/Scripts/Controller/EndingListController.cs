using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingListController : MonoBehaviour
{
    public MenuView EndingListView;
    public PopupController CreditController;
    public DialogueController DialogueController;

    void Update() {
        if(Input.GetButtonDown("Cancel")) {
            Close();
            return;
        }

        float y = Input.GetAxisRaw("Vertical");
        bool ydown = Input.GetButtonDown("Vertical");

        if(ydown) {
            if(y > 0f)
                EndingListView.SelectPrev();
            else
                EndingListView.SelectNext();
        }

        if(Input.GetButtonDown("Submit")) {
            switch(EndingListView.SelectorValue) {
                case 0:
                    ShowCredit();
                    break;
                case 1:
                    ShowEnding();
                    break;
                case 2:
                    Close();
                    break;
            }
        }
    }

    void OnEnable() {
        EndingListView.Show();
    }

    void OnDisable() {
        if(EndingListView)
            EndingListView.Hide();
    }

    public void ShowCredit() {
        GameManager.SetController(CreditController);
    }

    public void ShowEnding() {
        // TODO: 엔딩 관련 설정 모음 창으로
        GameManager.SetController(DialogueController);
        DialogueController.RunDialogueScript("Ending");
    }

    public void Close() {
        GameManager.UndoController();
        GameManager.Resume();
    }

}

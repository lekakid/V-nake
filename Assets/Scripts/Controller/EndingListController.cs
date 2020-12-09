using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingListController : MonoBehaviour
{
    public MenuView EndingListView;
    public PopupController CreditController;

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
        GameManager.PushController(this);
        CreditController.enabled = true;
    }

    public void ShowEnding() {
        // TODO : 엔딩 애니메이션 작업 끝나고 추가
    }

    public void Close() {
        this.enabled = false;
        GameManager.PopController();
        GameManager.Resume();
    }

}

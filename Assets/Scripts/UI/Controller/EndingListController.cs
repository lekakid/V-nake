using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingListController : MonoBehaviour
{
    public UIView CreditView;
    MenuView EndingListView;

    void Awake() {
        EndingListView = GetComponent<MenuView>();
    }

    void Update() {
        if(UIManager.Instance.Current != EndingListView)
            return;

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
                    ShowHiddenEnding();
                    break;
                case 3:
                    Close();
                    break;
            }
        }
    }

    public void ShowCredit() {
        UIManager.Instance.Push(CreditView);
    }

    public void ShowEnding() {
        // TODO : 엔딩 애니메이션 작업 끝나고 추가
    }

    public void ShowHiddenEnding() {
        // TODO : 엔딩 애니메이션 작업 끝나고 추가
    }

    public void Close() {
        UIManager.Instance.Pop();
        GameManager.Resume();
    }

}

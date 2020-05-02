using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayView : MonoBehaviour
{
    public TextMeshProUGUI Count;

    void Update() {
        if(GameManager.Instance.State != GameStateType.PLAY)
            return;

        if(Input.GetButtonDown("Cancel")) {
            GameManager.Instance.State = GameStateType.PAUSEMENU;
            return;
        }
    }

    public void Init() {
        SetCount(0);
    }

    public void SetCount(int n) {
        if(Count)
            Count.SetText(string.Format("x{0:000}", n));
    }
}

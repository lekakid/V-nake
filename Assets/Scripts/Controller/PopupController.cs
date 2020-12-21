using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    public CanvasView PopupView;

    void Update() {
        if(Input.anyKeyDown) {
            GameManager.UndoController();
        }
    }

    void OnEnable() {
        PopupView.Show();
    }

    void OnDisable() {
        PopupView.Hide();
    }
}

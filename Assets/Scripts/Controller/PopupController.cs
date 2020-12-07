using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    public CanvasView PopupView;

    void Update() {
        if(Input.anyKeyDown) {
            SoundManager.Instance.PlaySFX("Select");
            this.enabled = false;
            GameManager.PopController();
        }
    }

    void OnEnable() {
        PopupView.Show();
    }

    void OnDisable() {
        PopupView.Hide();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    CanvasView PopupView;

    void Awake() {
        PopupView = GetComponent<CanvasView>();
    }

    void Update() {
        if(Input.anyKeyDown) {
            SoundManager.Instance.PlaySFX("Select");
            // UIManager.Instance.Pop();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    UIView PopupView;

    void Awake() {
        PopupView = GetComponent<UIView>();
    }

    void Update() {
        if(UIManager.Instance.Current != PopupView) 
            return;

        if(Input.anyKeyDown) {
            SoundManager.Instance.PlaySFX("Select");
            UIManager.Instance.Pop();
        }
    }
}

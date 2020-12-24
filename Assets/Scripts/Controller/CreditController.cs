using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditController : MonoBehaviour
{
    public CanvasView CreditView;

    void Update() {
        if(Input.GetButtonDown("Cancel") || Input.GetButtonDown("Submit")) {
            Back();
        }
    }

    void OnEnable() {
        CreditView.Show();
    }

    void OnDisable() {
        CreditView.Hide();
    }

    public void Back() {
        GameManager.UndoController();
    }

    public void MuPixiv() {
        Application.OpenURL("https://www.pixiv.net/users/37519029");
    }

    public void LeKAKiDGitHub() {
        Application.OpenURL("https://github.com/lekakid/");
    }

    public void YaringYoutube() {
        Application.OpenURL("https://www.youtube.com/user/Yarings/");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public void OnIntroFinished() {
        TitleManager.Instance.OnIntroFinished();
    }
}

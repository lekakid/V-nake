using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueView : CanvasView
{
    public Text SpeakerName;
    public Text ContentScript;

    public void SetName(string name) {
        SpeakerName.text = name;
    }

    public void SetContent(string content) {
        ContentScript.text = content;
    }
}

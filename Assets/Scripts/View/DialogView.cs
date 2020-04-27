using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogView : MonoBehaviour
{
    Image Background;
    public GameObject Name;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI Content;

    void Start()
    {
        Background = GetComponent<Image>();
    }

    void SetName(string name) {
        NameText.SetText(name);
    }

    void PrintContent(string Text) {
        
    }
}

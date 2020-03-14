using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultItem : MonoBehaviour
{
    public Image img;
    public Text txt;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        txt = GetComponent<Text>();
    }
}

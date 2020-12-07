using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SnakeView : CanvasView
{
    public TextMeshProUGUI CountText;

    public void SetScore(int n) {
        CountText.SetText(string.Format("x{0:000}", n));
    }
}

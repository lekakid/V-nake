using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayView : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Count;

    public void SetCount(int n) {
        if(Count)
            Count.SetText(string.Format("x{0:000}", n));
    }
}

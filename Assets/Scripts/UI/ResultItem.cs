using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultItem : MonoBehaviour
{
    public Image Image;
    public TextMeshProUGUI Text;

    public void MakeItem(Transform Parent, Sprite Sprite, int Count) {
        Image.sprite = Sprite;
        Text.SetText(string.Format("x{0:00}", Count));

        transform.SetParent(Parent);
    }
}

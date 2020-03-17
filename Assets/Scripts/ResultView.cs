using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultView : MonoBehaviour
{
    public GameObject GameoverLogo;
    public GameObject Grid;
    public GameObject Item;
    
    public void Show() {
        gameObject.SetActive(true);

        // DoTween Animation
    }

    public void addItem(Sprite img, int count) {
        ResultItem i = Instantiate(Item.gameObject).GetComponent<ResultItem>();

        i.img.sprite = img;
        i.txt.text = string.Format("x{0:00}", count);;
        i.transform.SetParent(Grid.transform);
    }
}

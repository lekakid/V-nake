using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultView : MonoBehaviour
{
    [Header("Tweening")]
    public Image Background;
    public Image Gameover;

    [Header("Result Item")]
    public GameObject Grid;
    public GameObject Item;
    
    public void Show() {
        gameObject.SetActive(true);
        
        Sequence s = DOTween.Sequence();

        s.Append(Background.DOFade(0.4f, 1f));
        s.Append(Gameover.DOFade(1f, 1f));
        s.SetUpdate(true);

        s.Play();
    }

    public void addItem(Sprite img, int count) {
        ResultItem i = Instantiate(Item.gameObject).GetComponent<ResultItem>();

        i.img.sprite = img;
        i.txt.text = string.Format("x{0:00}", count);;
        i.transform.SetParent(Grid.transform);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultView : MonoBehaviour
{
    [Header("Tweening")]
    public Animator Animator;

    [Header("Result Item")]
    public GameObject Grid;
    public GameObject Item;
    
    public void Show() {
        gameObject.SetActive(true);
        
        Animator.SetTrigger("Show");
    }

    public void addItem(Sprite sprite, int count) {
        ResultItem i = Instantiate(Item.gameObject).GetComponent<ResultItem>();

        i.MakeItem(Grid.transform, sprite, count);
    }
}

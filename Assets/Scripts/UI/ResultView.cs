﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultView : MonoBehaviour
{
    [Header("Result Item")]
    public Transform Grid;
    public GameObject CountPrefab;

    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void Show() {
        List<Character> list = SpawnManager.Instance.CharacterList;
        Dictionary<Character, int> dic = SpawnManager.Instance.RescueCount;

        foreach(Character c in list) {
            if(dic[c] > 0) {
                GameObject i = Instantiate(CountPrefab);
                i.GetComponentInChildren<Image>().sprite = c.SpriteRenderer.sprite;
                i.GetComponentInChildren<TextMeshProUGUI>().SetText(string.Format("x{0:00}", dic[c]));
                i.transform.SetParent(Grid);
            }
        }

        _animator.SetTrigger("Show");
    }
}

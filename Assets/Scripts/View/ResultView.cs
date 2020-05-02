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

    public void Init() {
        gameObject.SetActive(false);
        
        for(int i = Grid.childCount - 1; i >= 0; i--) {
            Destroy(Grid.GetChild(i).gameObject);
        }
    }
    
    public void Show() {
        SnakeManager manager = GameManager.Instance.CurrentManager.GetComponent<SnakeManager>();
        SpawnController spawner = manager.SpawnController;
        List<Character> list = spawner.CharacterList;
        Dictionary<Character, int> dic = spawner.RescueCount;

        foreach(Character c in list) {
            if(dic[c] > 0) {
                GameObject i = Instantiate(CountPrefab);
                i.GetComponentInChildren<Image>().sprite = c.SpriteRenderer.sprite;
                i.GetComponentInChildren<TextMeshProUGUI>().SetText(string.Format("x{0:00}", dic[c]));
                i.transform.SetParent(Grid);
            }
        }

        gameObject.SetActive(true);
    }
}

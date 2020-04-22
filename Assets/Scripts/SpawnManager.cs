﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnRate {
        public ClassType Class;
        public float Rate;
    }

    [Header("Object")]
    public PlayView PlayView;

    [Header("Character")]
    public List<SpawnRate> SpawnRateList;
    public List<Character> CharacterList;
    
    public Dictionary<Character, int> RescueCount;
    public static SpawnManager Instance { get; private set; }
    public int TotalRescueCount { get; private set; }

    void Awake()
    {
        Instance = this;

        RescueCount = new Dictionary<Character, int>();
        foreach(Character c in CharacterList) {
            RescueCount.Add(c, 0);
        }
    }

    public void Init() {
        TotalRescueCount = 0;

        foreach(Character c in CharacterList) {
            RescueCount[c] = 0;
        }
    }
    
    public GameObject SpawnCharacter() {
        float r = Random.Range(0f, 100f);

        int i = 0;

        while(r >= SpawnRateList[i].Rate) {
            r -= SpawnRateList[i++].Rate;
        }

        List<Character> list = CharacterList.FindAll(c=>c.Class==SpawnRateList[i].Class);
        Character result = list[(int)Random.Range(0, list.Count)];

        RescueCount[result]++;
        TotalRescueCount++;
        PlayView.SetCount(TotalRescueCount);
        
        return Instantiate(result.gameObject);
    }
}

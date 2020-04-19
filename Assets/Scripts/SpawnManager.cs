using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Character")]
    public List<Rarity> SpawnRate;
    public List<Character> CharacterList;
    public Dictionary<Character, int> RescueCount;
    
    public static SpawnManager Instance {
        get { return _instance; }
    }

    public int TotalRescueCount {
        get { return _totalRescueCount; }
    }

    static SpawnManager _instance;
    
    int _totalRescueCount = 0;

    void Awake()
    {
        _instance = this;

        RescueCount = new Dictionary<Character, int>();
        foreach(Character c in CharacterList) {
            RescueCount.Add(c, 0);
        }
    }
    
    public GameObject SpawnCharacter() {
        float r = Random.Range(0f, 100f);

        int i = 0;

        while(r >= SpawnRate[i].Rate) {
            r -= SpawnRate[i++].Rate;
        }

        List<Character> list = CharacterList.FindAll(c=>c.Rarity==SpawnRate[i].Grade);
        Character result = list[(int)Random.Range(0, list.Count)];

        RescueCount[result]++;
        _totalRescueCount++;
        
        return Instantiate(result.gameObject);
    }
}

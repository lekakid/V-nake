using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [Header("Character")]
    public List<Rarity> Rarity;
    public List<Character> CharacterList;
    public Dictionary<Character, int> SpawnCount = new Dictionary<Character, int>();
    
    public int RescueCount {
        get { return _rescueCount; }
    }
    
    int _rescueCount = 0;
    
    public GameObject SpawnCharacter() {
        float r = Random.Range(0f, 100f);

        int i = 0;

        while(r >= Rarity[i].Rate) {
            r -= Rarity[i++].Rate;
        }

        List<Character> list = CharacterList.FindAll(c=>c.Rarity==Rarity[i].Grade);
        Character result = list[(int)Random.Range(0, list.Count)];
        
        if(!SpawnCount.ContainsKey(result)) {
            SpawnCount.Add(result, 0);
        }

        SpawnCount[result]++;
        _rescueCount++;
        
        return Instantiate(result.gameObject);
    }
}

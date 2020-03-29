using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [Header("View")]
    public PlayView PlayView;

    [Header("Character")]
    public List<Rarity> Rarity;
    public List<Character> CharacterList;
    public Dictionary<Character, int> SpawnCount = new Dictionary<Character, int>();
    public int TotalCount = 0;
    
    public GameObject SpawnCharacter() {
        float r = Random.Range(0f, 100f);

        int i = 0;

        while(r >= Rarity[i].Rate) {
            r -= Rarity[i++].Rate;
        }

        List<Character> list = CharacterList.FindAll(x=>x.Rarity==Rarity[i].Grade);
        Character c = list[(int)Random.Range(0, list.Count)];
        
        if(!SpawnCount.ContainsKey(c)) {
            SpawnCount.Add(c, 0);
        }

        SpawnCount[c]++;
        TotalCount++;

        PlayView.SetCount(TotalCount);
        
        return Instantiate(c.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public List<Rarity> Rarity;
    public List<Character> CharacterList;
    public Dictionary<Character, int> CharacterCount = new Dictionary<Character, int>();
    
    public GameObject SpawnCharacter() {
        float r = Random.Range(0f, 100f);

        int i = 0;

        while(r >= Rarity[i].Rate) {
            i++;
        }

        List<Character> list = CharacterList.FindAll(x=>x.Rarity==Rarity[i].Grade);
        Character c = list[(int)Random.Range(0, list.Count - 1)];
        
        if(!CharacterCount.ContainsKey(c)) {
            CharacterCount.Add(c, 0);
        }

        CharacterCount[c]++;
        
        return Instantiate(c.gameObject);
    }
}

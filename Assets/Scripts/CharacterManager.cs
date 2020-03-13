using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    /*
    public float NormalRarity;
    public float UniqueRarity;
    public float LegendaryRarity;

    public List<GameObject> Normal;
    public List<GameObject> Unique;
    public List<GameObject> Legendary;

    public float NormalCount = 0;
    public float UniqueCount = 0;
    public float LegendaryCount = 0;
    */

    public List<Rarity> Rarity;
    public List<Character> CharacterList;
    
    public GameObject SpawnCharacter() {
        float r = Random.Range(0f, 100f);

        int i = 0;

        while(r >= Rarity[i].Rate) {
            i++;
        }

        List<Character> list = CharacterList.FindAll(x=>x.Rarity==Rarity[i].Grade);
        
        return Instantiate(list[(int)Random.Range(0, list.Count - 1)].gameObject);
    /*
        if(r < NormalRarity) {
            NormalCount++;
            return Instantiate(Normal[(int)Random.Range(0, Normal.Count - 1)]);
        }

        r -= NormalRarity;
        
        if(r < UniqueRarity) {
            UniqueCount++;
            return Instantiate(Unique[(int)Random.Range(0, Unique.Count - 1)]);
        }

        LegendaryCount++;
        return Instantiate(Legendary[(int)Random.Range(0, Legendary.Count - 1)]);
        */
    }
}

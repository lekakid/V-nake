using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public float NormalRarity;
    public float UniqueRarity;
    public float LegendaryRarity;

    public List<GameObject> Normal;
    public List<GameObject> Unique;
    public List<GameObject> Legendary;

    public GameObject SpawnCharacter() {
        float r = Random.Range(0f, 100f);

        if(r < NormalRarity) {
            Debug.Log("[CharacterManager/MakeChracter] Normal Character");
            return Instantiate(Normal[(int)Random.Range(0, Normal.Count - 1)]);
        }

        r -= NormalRarity;
        
        if(r < UniqueRarity) {
            Debug.Log("[CharacterManager/MakeChracter] Unique Character");
            return Instantiate(Unique[(int)Random.Range(0, Unique.Count - 1)]);
        }

        r -= UniqueRarity;

        if(r < LegendaryRarity) {
            Debug.Log("[CharacterManager/MakeChracter] Legendary Character");
            return Instantiate(Legendary[(int)Random.Range(0, Legendary.Count - 1)]);
        }

        return null;
    }
}

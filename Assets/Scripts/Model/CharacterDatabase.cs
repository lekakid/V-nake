using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDatabase : MonoBehaviour
{
    public List<SpawnRate> SpawnRates;

    List<CharacterScriptableObject> _Characters;

    [System.Serializable]
    public struct SpawnRate {
        public CharacterRarityType Rarity;
        public float Rate;
    }

    void Awake() {
        CharacterScriptableObject[] list = Resources.LoadAll<CharacterScriptableObject>("Character");
        _Characters = new List<CharacterScriptableObject>(list);
    }

    public CharacterScriptableObject GetCharacterData(string name) {
        return _Characters.Find(x => x.name == name);
    }

    public CharacterScriptableObject GetRandomCharacterData() {
        float totalRate = 0f;
        foreach(SpawnRate rate in SpawnRates) {
            totalRate += rate.Rate;
        }

        float r = Random.Range(0f, totalRate);

        int i = 0;
        while(r >= SpawnRates[i].Rate) {
            r -= SpawnRates[i++].Rate;
        }

        List<CharacterScriptableObject> spawntable = _Characters.FindAll(c=>c.Rarity==SpawnRates[i].Rarity);
        CharacterScriptableObject result = spawntable[(int)Random.Range(0, spawntable.Count)];

        return result;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnRate {
        public CharacterRarityType Rarity;
        public float Rate;
    }

    [Header("Character")]
    public Character Prefab;
    public List<SpawnRate> SpawnRateList;
    public List<CharacterData> CharacterList;
    
    private int _totalRescueCount;
    public int TotalRescueCount { 
        get { return _totalRescueCount; }
        private set {
            _totalRescueCount = value;
            GameManager.Instance.PlayView.SetCount(_totalRescueCount);
        }
    }

    public Dictionary<CharacterData, int> RescueCount;

    void Awake()
    {
        RescueCount = new Dictionary<CharacterData, int>();
        foreach(CharacterData c in CharacterList) {
            RescueCount.Add(c, 0);
        }
    }

    public void Init() {
        TotalRescueCount = 0;

        foreach(CharacterData c in CharacterList) {
            RescueCount[c] = 0;
        }
    }
    
    public Character SpawnCharacter() {
        float r = Random.Range(0f, 100f);

        int i = 0;

        while(r >= SpawnRateList[i].Rate) {
            r -= SpawnRateList[i++].Rate;
        }

        List<CharacterData> spawntable = CharacterList.FindAll(c=>c.Rarity==SpawnRateList[i].Rarity);
        CharacterData data = spawntable[(int)Random.Range(0, spawntable.Count)];

        RescueCount[data]++;
        TotalRescueCount++;

        Character instance = Instantiate(Prefab.gameObject).GetComponent<Character>();
        instance.Data = data;
        
        return instance;
    }
}

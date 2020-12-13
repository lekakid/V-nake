using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDatabase : MonoBehaviour
{
    public List<SpawnRate> SpawnRates;
    public List<CharacterScriptableObject> Characters;

    public Dictionary<string, int> CurrentRescueScore;
    public Dictionary<string, int> TotalRescueScore;

    List<string> _characterNames;

    [System.Serializable]
    public struct SpawnRate {
        public CharacterRarityType Rarity;
        public float Rate;
    }

    void Awake() {
        CurrentRescueScore = new Dictionary<string, int>();
        TotalRescueScore = new Dictionary<string, int>();
        _characterNames = new List<string>();

        foreach(CharacterScriptableObject o in Characters) {
            _characterNames.Add(o.name);
            CurrentRescueScore.Add(o.name, 0);
            TotalRescueScore.Add(o.name, 0);
        }

        foreach(string n in _characterNames) {
            TotalRescueScore[n] = PlayerPrefs.GetInt("Score." + n, 0);
        }
    }

    public CharacterScriptableObject GetCharacterData(string name) {
        return Characters.Find(x => x.name == name);
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

        List<CharacterScriptableObject> spawntable = Characters.FindAll(c=>c.Rarity==SpawnRates[i].Rarity);
        CharacterScriptableObject result = spawntable[(int)Random.Range(0, spawntable.Count)];

        return result;
    }

    public void AddPoint(string name) {
        CurrentRescueScore[name]++;
    }

    public int GetScore(string name) {
        return PlayerPrefs.GetInt($"Score.{name}");
    }

    public int GetScoreSum() {
        int result = 0;

        foreach(string n in _characterNames) {
            result += CurrentRescueScore[n];
        }

        return result;
    }

    public void UpdateScore() {
        foreach(string n in _characterNames) {
            TotalRescueScore[n] += CurrentRescueScore[n];
            CurrentRescueScore[n] = 0;
        }

        foreach(string n in _characterNames) {
            PlayerPrefs.SetInt("Score." + n, TotalRescueScore[n]);
        }
    }
}

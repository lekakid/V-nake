using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Status
{
    public int CurrentWalkCount;
    public int CurrentRescueCount;
    public Dictionary<string, int> CurrentCharacterRescueCounts;

    public int MaxWalkCount;
    public int TotalWalkCount;

    public int MaxRescueCount;
    public int TotalRescueCount;
    public Dictionary<string, int> CharacterRescueCounts;

    public int PlayCount;
    public float PlayTime;

    public bool Ending;

    private static Status _instance;
    public static Status Instance {
        get {
            if(_instance == null) {
                _instance = new Status();
            }
            return _instance;
        }
    }

    public Status() {
        CurrentCharacterRescueCounts = new Dictionary<string, int>();
        CharacterRescueCounts = new Dictionary<string, int>();
        CharacterScriptableObject[] characters = Resources.LoadAll<CharacterScriptableObject>("Character");
        
        foreach(CharacterScriptableObject character in characters) {
            CurrentCharacterRescueCounts.Add(character.name, 0);
            CharacterRescueCounts.Add(character.name, 0);
        }

        Load();
    }

    public void Initialize() {
        CurrentWalkCount = 0;
        CurrentRescueCount = 0;
        List<string> keys = new List<string>(CurrentCharacterRescueCounts.Keys);
        foreach(string key in keys) {
            CurrentCharacterRescueCounts[key] = 0;
        }
    }

    public void Load() {
        CurrentRescueCount = 0;
        MaxRescueCount = PlayerPrefs.GetInt("Status.MaxRescueCount", 0);
        TotalRescueCount = PlayerPrefs.GetInt("Status.TotalRescueCount", 0);
        List<string> keys = new List<string>(CharacterRescueCounts.Keys);
        foreach(string key in keys) {
            CharacterRescueCounts[key] = PlayerPrefs.GetInt($"Status.Score.{key}", 0);
        }

        CurrentWalkCount = 0;
        MaxWalkCount = PlayerPrefs.GetInt("Status.MaxWalkCount", 0);
        TotalWalkCount = PlayerPrefs.GetInt("Status.TotalWalkCount", 0);

        PlayCount = PlayerPrefs.GetInt("Status.PlayCount", 0);
        PlayTime = PlayerPrefs.GetFloat("Status.PlayTime", 0f);
        
        Ending = PlayerPrefs.GetInt("Status.Ending", 0) == 1;
    }

    public void Save() {
        if(CurrentRescueCount > MaxRescueCount) {
            MaxRescueCount = CurrentRescueCount;
        }
        if(CurrentWalkCount > MaxWalkCount) {
            MaxWalkCount = CurrentWalkCount;
        }

        PlayerPrefs.SetInt("Status.MaxRescueCount", MaxRescueCount);
        List<string> keys = new List<string>(CharacterRescueCounts.Keys);
        foreach(string key in keys) {
            CharacterRescueCounts[key] += CurrentCharacterRescueCounts[key];
            PlayerPrefs.SetInt($"Status.Score.{key}", CharacterRescueCounts[key]);
        }
        TotalRescueCount += CurrentRescueCount;
        PlayerPrefs.SetInt("Status.TotalRescueCount", TotalRescueCount);

        PlayerPrefs.SetInt("Status.MaxWalkCount", MaxWalkCount);
        TotalWalkCount += CurrentWalkCount;
        PlayerPrefs.SetInt("Status.TotalWalkCount", TotalWalkCount);

        PlayerPrefs.SetInt("Status.PlayCount", PlayCount);
        PlayerPrefs.SetFloat("Status.PlayTime", PlayTime);

        PlayerPrefs.SetInt("Status.Ending", Ending ? 1 : 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterRarityType { B, A, S, SS }

[CreateAssetMenu(fileName = "New Character Data", menuName = "V-nake/Character Data")]
public class CharacterScriptableObject : ScriptableObject
{
    public GameObject CharacterPrefab;
    public Sprite Image;
    public CharacterRarityType Rarity;
}

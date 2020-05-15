using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterRarityType { B, A, S, SS }

[CreateAssetMenu(fileName = "New Character Data", menuName = "Character/Data")]
public class CharacterData : ScriptableObject
{
    public Sprite Image;
    public CharacterRarityType Rarity;
    public RuntimeAnimatorController AnimationController;
}

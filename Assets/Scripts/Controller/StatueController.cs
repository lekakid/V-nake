using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueController : MonoBehaviour
{
    public SpriteRenderer CharacterRenderer;
    public CharacterDatabase CharacterDatabase;

    void Awake() {
        if(CharacterDatabase.GetScore(name) > 0) {
            CharacterScriptableObject data = CharacterDatabase.GetCharacterData(name);
            CharacterRenderer.sprite = data.Image;
        }
    }
}
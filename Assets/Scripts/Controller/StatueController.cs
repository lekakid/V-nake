using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueController : MonoBehaviour
{
    public SpriteRenderer CharacterRenderer;
    public CharacterDatabase CharacterDatabase;

    InteractionData InteractionData;

    void Awake() {
        InteractionData = GetComponent<InteractionData>();
        
        if(Status.Instance.CharacterRescueCounts[name] > 0) {
            CharacterScriptableObject data = CharacterDatabase.GetCharacterData(name);
            CharacterRenderer.sprite = data.Image;
        }
        else {
            InteractionData.DialogueKey = "GalleryUnknown";
        }
    }
}
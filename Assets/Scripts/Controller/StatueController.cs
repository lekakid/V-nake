using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueController : MonoBehaviour
{
    public Transform Player;
    public CharacterDatabase CharacterDatabase;

    SpriteRenderer _characterRenderer;
    SpriteRenderer _baseRenderer;
    InteractionData _interactionData;

    void Awake() {
        _baseRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _characterRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        _interactionData = GetComponent<InteractionData>();
        
        if(Status.Instance.CharacterRescueCounts[name] > 0) {
            CharacterScriptableObject data = CharacterDatabase.GetCharacterData(name);
            _characterRenderer.sprite = data.Image;
        }
        else {
            _interactionData.DialogueKey = "GalleryUnknown";
        }
    }

    void Update() {
        _characterRenderer.sortingOrder = (Player.position.y > transform.position.y) ? 10 : 0;
        _baseRenderer.sortingOrder = (Player.position.y > transform.position.y) ? 10 : 0;
    }
}
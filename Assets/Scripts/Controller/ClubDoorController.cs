using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubDoorController : MonoBehaviour
{
    public GameObject Door;
    public GameObject DoorDialogue;
    public Transform Player;
    public SpriteRenderer OpenedDoorRenderer;

    void Awake() {
        int brownieCount = Status.Instance.CharacterRescueCounts["Brownie"];
        int goldenBrownieCount = Status.Instance.CharacterRescueCounts["GoldenBrownie"];

        if(brownieCount >= 500 && goldenBrownieCount >= 1) {
            Door.SetActive(false);
            DoorDialogue.SetActive(false);
        }
    }

    void Update() {
        OpenedDoorRenderer.sortingOrder = (Player.position.y > transform.position.y) ? 10 : 0;
    }
}

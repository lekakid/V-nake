using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryController : MonoBehaviour
{
    public GameObject Player;
    public GalleryPauseController GalleryPauseController;
    public EndingListController EndingListController;
    public DialogueController DialogueController;

    Rigidbody2D rb;
    Vector2 lastDirection;

    void Awake() {
        rb = Player.GetComponent<Rigidbody2D>();

        GameManager.SetController(this);
    }

    void Update() {
        if(Input.GetButtonDown("Cancel")) {
            ShowPause();
            return;
        }

        if(Input.GetButtonDown("Submit")) {
            if(Interact())
                return;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 input = new Vector2(x, (x == 0) ? y: 0);
        rb.velocity = input * 5f;
        if(input.magnitude != 0f)
            lastDirection = input;
    }

    bool Interact() {
        Vector2 target = (Vector2)Player.transform.position + lastDirection;
        Collider2D collider = Physics2D.OverlapBox(target, new Vector2(0.5f, 0.5f), 0f, (1 << 8));

        if(collider) {
            switch(collider.tag) {
                case "UI_ENDINGLIST":
                    ShowEndingList();
                    break;
                case "DIALOGUE_OBJECT":
                    Talk(collider.gameObject);
                    break;
            }
            return true;
        }
        
        return false;
    }

    void ShowPause() {
        GameManager.SetController(GalleryPauseController);
        GameManager.Pause();
    }

    void ShowEndingList() {
        GameManager.SetController(EndingListController);
        GameManager.Pause();
    }

    void Talk(GameObject obj) {
        GameManager.SetController(DialogueController);
        DialogueController.RunDialogueScript(obj.GetComponent<InteractionData>().DialogueKey);
    }
}
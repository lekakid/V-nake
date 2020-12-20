using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryController : MonoBehaviour
{
    public GameObject Player;
    public GalleryPauseController GalleryPauseController;
    public EndingListController EndingListController;
    public DialogueController DialogueController;

    public Transform ClubPoint;
    public Transform GalleryPoint;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Vector2 lastDirection;

    void Awake() {
        rb = Player.GetComponent<Rigidbody2D>();
        spriteRenderer = Player.GetComponent<SpriteRenderer>();

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

        if(x < 0) {
            spriteRenderer.flipX = false;
        }
        else if(x > 0) {
            spriteRenderer.flipX = true;
        }

        Vector2 input = new Vector2(x, (x == 0) ? y: 0);
        rb.velocity = input * 5f;
        if(input.magnitude != 0f)
            lastDirection = input;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "UI_RETURN_TITLE") {
            GameManager.LoadScene("Title");
        }

        if(other.tag == "TELEPORT_CLUB") {
            transform.position = ClubPoint.position;
        }

        if(other.tag == "TELEPORT_GALLERY") {
            transform.position = GalleryPoint.position;
        }
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
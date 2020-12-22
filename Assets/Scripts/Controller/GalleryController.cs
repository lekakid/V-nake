using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryController : MonoBehaviour
{
    public GameObject Player;
    public GameObject InteractableMark;

    public GalleryPauseController GalleryPauseController;
    public EndingListController EndingListController;
    public DialogueController DialogueController;

    public Transform ClubPoint;
    public Transform GalleryPoint;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    GameObject visitedDialogueObject;
    MonoBehaviour visitedController;

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
            if(Interact()) {
                rb.velocity = Vector2.zero;
                return;
            }
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
        rb.velocity = input * 7f;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "DIALOGUE_OBJECT") {
            visitedDialogueObject = other.gameObject;
            InteractableMark.SetActive(true);
        }

        if(other.tag == "UI_ENDINGLIST") {
            visitedController = EndingListController;
            InteractableMark.SetActive(true);
        }

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

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "DIALOGUE_OBJECT") {
            visitedDialogueObject = null;
            InteractableMark.SetActive(false);
        }

        if(other.tag == "UI_ENDINGLIST") {
            visitedController = null;
            InteractableMark.SetActive(false);
        }
    }

    bool Interact() {
        if(visitedDialogueObject) {
            Talk(visitedDialogueObject);
            return true;
        }

        if(visitedController) {
            GameManager.SetController(visitedController);
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
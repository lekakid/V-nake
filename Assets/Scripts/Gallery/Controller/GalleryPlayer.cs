using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 lastDirection;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if(UIManager.Instance.Current != null)
            return;

        if(Input.GetButtonDown("Submit")) {
            if(tryTalk())
                return;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 input = new Vector2(x, (x == 0) ? y: 0);
        rb.velocity = input * 5f;
        lastDirection = input;
    }

    bool tryTalk() {
        Vector2 target = (Vector2)transform.position + lastDirection;
        Collider2D collider = Physics2D.OverlapBox(target, new Vector2(1f, 1f), 0f);

        Debug.Log(collider.name);
        
        return false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    public GameManager manager;
    public Character Follower = null;

    float walkTime = 0.15f;
    Vector2 inputDir = Vector2.right;
    Vector2 prevWalkDir = Vector2.right;
    Vector2 currentWalkDir = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Follower = null) {
            if(Input.GetAxisRaw("Vertical") > 0 & !currentWalkDir.Equals(Vector2.down))
                inputDir = Vector2.up;

            if(Input.GetAxisRaw("Vertical") < 0 & !currentWalkDir.Equals(Vector2.up))
                inputDir = Vector2.down;

            if(Input.GetAxisRaw("Horizontal") > 0 & !currentWalkDir.Equals(Vector2.left))
                inputDir = Vector2.right;

            if(Input.GetAxisRaw("Horizontal") < 0 & !currentWalkDir.Equals(Vector2.right))
                inputDir = Vector2.left;
        }
        else {
            inputDir = Follower.prevWalkDir;
        }
    }

    IEnumerator Move() {
        while(true) {
            prevWalkDir = currentWalkDir;
            currentWalkDir = inputDir;
            Vector3 v = transform.position + (Vector3)currentWalkDir;
            transform.DOMove(v, walkTime).SetEase(Ease.Linear);
            yield return new WaitForSeconds(walkTime);
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
    }
}

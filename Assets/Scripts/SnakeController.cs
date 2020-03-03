using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SnakeController : MonoBehaviour
{
    public Head Head;
    public Transform Bush;
    public float WalkDelay;
    public float SpawnDelay;

    public CharacterManager CharacterManager;

    enum Dir { UP, DOWN, RIGHT, LEFT };
    Vector2 inputDir = Vector2.zero;
    Dir walkDir;
    Vector2 tailPos = Vector2.zero;
    List<Character> body = new List<Character>();
    List<Vector2> bodypos = new List<Vector2>();

    bool adding = false;

    // Start is called before the first frame update
    void Start()
    {
        body.Add(Head);
        bodypos.Add(new Vector2(-10, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") > 0 & walkDir != Dir.DOWN) {
            inputDir = Vector2.up;
        }

        if (Input.GetAxisRaw("Vertical") < 0 & walkDir != Dir.UP) {
            inputDir = Vector2.down;
        }

        if (Input.GetAxisRaw("Horizontal") > 0 & walkDir != Dir.LEFT) {
            inputDir = Vector2.right;
        }

        if (Input.GetAxisRaw("Horizontal") < 0 & walkDir != Dir.RIGHT) {
            inputDir = Vector2.left;
        }
    }

    IEnumerator SnakeMove() {
        while(true) {
            yield return new WaitUntil(() => !adding);

            Vector2 NextPos = bodypos[0] + inputDir;
            bodypos.Insert(0, NextPos);
            tailPos = bodypos[bodypos.Count - 1];
            bodypos.RemoveAt(bodypos.Count - 1);

            if(inputDir == Vector2.up)
                walkDir = Dir.UP;
            if(inputDir == Vector2.down)
                walkDir = Dir.DOWN;
            if(inputDir == Vector2.right)
                walkDir = Dir.RIGHT;
            if(inputDir == Vector2.left)
                walkDir = Dir.LEFT;

            for(int i = 0; i < bodypos.Count; i++) {
                body[i].Walk(bodypos[i], WalkDelay);
            }
            yield return new WaitForSeconds(WalkDelay);
        }
    }

    public void AddTail() {
        adding = true;

        Character tail = CharacterManager.SpawnCharacter().GetComponent<Character>();
        tail.transform.position = Bush.position;
        tail.Spawn();
        
        body.Add(tail);
        bodypos.Add(tailPos);

        adding = false;
    }

    public void PlayGame() {
        StartCoroutine("SnakeMove");
    }

    public void StopGame() {
        StopCoroutine("SnakeMove");
    }
}

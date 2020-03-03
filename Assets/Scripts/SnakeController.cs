using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SnakeController : MonoBehaviour
{
    public GameObject Head;
    public Transform Bush;
    public float WalkDelay;

    public CharacterManager CharacterManager;

    enum Dir { UP, DOWN, RIGHT, LEFT };
    Vector2 inputDir = Vector2.zero;
    Dir walkDir;
    Vector2 tailPos = Vector2.zero;
    List<GameObject> body = new List<GameObject>();
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
            walkDir = Dir.UP;
        }

        if (Input.GetAxisRaw("Vertical") < 0 & walkDir != Dir.UP) {
            inputDir = Vector2.down;
            walkDir = Dir.UP;
        }

        if (Input.GetAxisRaw("Horizontal") > 0 & walkDir != Dir.LEFT) {
            inputDir = Vector2.right;
            walkDir = Dir.RIGHT;
        }

        if (Input.GetAxisRaw("Horizontal") < 0 & walkDir != Dir.RIGHT) {
            inputDir = Vector2.left;
            walkDir = Dir.LEFT;
        }
    }

    IEnumerator SnakeMove() {
        while(true) {
            yield return new WaitUntil(() => !adding);

            Vector2 NextPos = bodypos[0] + inputDir;
            bodypos.Insert(0, NextPos);
            tailPos = bodypos[bodypos.Count - 1];
            bodypos.RemoveAt(bodypos.Count - 1);

            for(int i = 0; i < bodypos.Count; i++) {
                float d = bodypos[i].x - body[i].transform.position.x;
                if(d > 0)
                    body[i].GetComponent<SpriteRenderer>().flipX = true;
                else if(d < 0)
                    body[i].GetComponent<SpriteRenderer>().flipX = false;
                body[i].transform.DOMove(bodypos[i], WalkDelay).SetEase(Ease.Linear);
            }
            yield return new WaitForSeconds(WalkDelay);
        }
    }

    public void AddTail() {
        adding = true;

        GameObject tail = CharacterManager.SpawnCharacter();
        tail.transform.position = Bush.position;
        
        body.Add(tail);
        bodypos.Add(tailPos);

        adding = false;
    }
}

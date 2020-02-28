using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valkyrie : MonoBehaviour
{
    public float walkTime = 1;

    Vector2 walkDirection = Vector2.right;
    List<Transform> tails = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Vertical") > 0)
            walkDirection = Vector2.up;

        if(Input.GetAxisRaw("Vertical") < 0)
            walkDirection = Vector2.down;

        if(Input.GetAxisRaw("Horizontal") > 0)
            walkDirection = Vector2.right;

        if(Input.GetAxisRaw("Horizontal") < 0)
            walkDirection = Vector2.left;
    }

    void Move() {
        
    }
}

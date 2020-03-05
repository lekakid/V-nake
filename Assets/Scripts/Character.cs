using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    public bool Spawned = true;
    
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Spawn() {
        transform.DOMove(transform.position + Vector3.up * 0.3f, 0.3f)
                 .SetEase(Ease.OutSine)
                 .OnComplete(()=>{Spawned = false;});
    }

    public void Walk(Vector2 Dir, float Delay) {
        if(Spawned)
            return;

        float x = Dir.x - transform.position.x;
        if(x > 0) {
            sprite.flipX = true;
        }
        else if(x < 0) {
            sprite.flipX = false;
        }

        transform.DOMove(Dir, Delay).SetEase(Ease.Linear);
    }
}

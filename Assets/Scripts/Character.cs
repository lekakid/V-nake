using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    public Rarity.RarityEnum Rarity;
    public bool Spawned = true;
    
    public SpriteRenderer SpriteRenderer;

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
            SpriteRenderer.flipX = true;
        }
        else if(x < 0) {
            SpriteRenderer.flipX = false;
        }

        transform.DOMove(Dir, Delay).SetEase(Ease.Linear);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum ClassType { B, A, S, SS }

public class Character : MonoBehaviour
{
    public ClassType Class;
    public SpriteRenderer SpriteRenderer;
    
    bool _spawned;

    public void Spawn(int order) {
        _spawned = true;
        transform.DOMove(transform.position + Vector3.up * 0.3f, 0.3f)
                 .SetEase(Ease.OutSine)
                 .OnComplete(()=>{_spawned = false; SpriteRenderer.sortingOrder = order;});
    }

    public void Walk(Vector2 Dir, float Delay) {
        if(_spawned)
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

    public void CancelWalk() {
        transform.DOKill();
    }

    public void Remove() {
        CancelWalk();
        Destroy(gameObject);
    }
}

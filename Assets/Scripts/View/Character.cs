using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class Character : MonoBehaviour
{
    private CharacterData _data;
    public CharacterData Data {
        get { return _data; }
        set {
            _data = value;
            _spriteRenderer.sprite = _data.Image;
            _animator.runtimeAnimatorController = _data.AnimationController;
        }
    }

    SpriteRenderer _spriteRenderer;
    Animator _animator;
    bool _spawned;

    public void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    public void Spawn(int order) {
        _spawned = true;
        transform.DOMove(transform.position + Vector3.up * 0.3f, 0.3f)
                 .SetEase(Ease.OutSine)
                 .OnComplete(()=>{_spawned = false; _spriteRenderer.sortingOrder = order;});
    }

    public void Walk(Vector2 Dir, float Delay) {
        if(_spawned)
            return;

        float x = Dir.x - transform.position.x;
        if(x > 0) {
            _spriteRenderer.flipX = true;
        }
        else if(x < 0) {
            _spriteRenderer.flipX = false;
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

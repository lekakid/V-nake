using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasView : MonoBehaviour
{
    Animator _animator;
    public bool isAnimating { get; private set; }

    void Awake() {
        _animator = GetComponent<Animator>();
        
        RectTransform rect = GetComponent<RectTransform>();
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void OnAnimationEnter() {
        isAnimating = true;
    }

    public void OnAnimationExit() {
        isAnimating = false;
        _animator.speed = 1f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public class CanvasView : MonoBehaviour
{
    public bool isAnimating { get; private set; }

    CanvasGroup _canvasGroup;
    Animator _animator;

    void Awake() {
        _canvasGroup = GetComponent<CanvasGroup>();
        _animator = GetComponent<Animator>();
        
        RectTransform rect = GetComponent<RectTransform>();
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        Hide();
    }

    public void Show() {
        _animator.SetBool("Enabled", true);
    }

    public void Hide() {
        _animator.SetBool("Enabled", false);
    }

    public void Skip() {
        _animator.speed = 500f;
    }

    public void OnAnimationEnter() {
        isAnimating = true;
    }

    public void OnAnimationExit() {
        isAnimating = false;
        _animator.speed = 1f;
    }
}

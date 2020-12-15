using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasView : MonoBehaviour
{
    CanvasGroup _canvasGroup;
    Animator _animator;
    public bool hasShowAnimation;
    public bool hasHideAnimation;
    public bool isAnimating { get; private set; }

    void Awake() {
        _canvasGroup = GetComponent<CanvasGroup>();
        _animator = GetComponent<Animator>();
        
        RectTransform rect = GetComponent<RectTransform>();
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        Hide();
    }

    public void Show() {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        if(_animator && hasShowAnimation) {
            _animator.SetTrigger("Show");
        }
    }

    public void Hide() {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        if(_animator && hasHideAnimation) {
            _animator.SetTrigger("Hide");
        }
    }

    public void OnAnimationEnter() {
        isAnimating = true;
    }

    public void OnAnimationExit() {
        isAnimating = false;
        _animator.speed = 1f;
    }
}

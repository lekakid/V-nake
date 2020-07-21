using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour
{
    Animator _animator;

    [SerializeField] bool RootView = false;
    
    public bool isAnimating { get; private set; }

    void Awake() {
        _animator = GetComponent<Animator>();
        
        RectTransform rect = GetComponent<RectTransform>();
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        if(!RootView) {
            gameObject.SetActive(false);
        }
    }

    void Start() {
        if(RootView)
            UIManager.Instance.Push(this);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        if(!RootView)
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

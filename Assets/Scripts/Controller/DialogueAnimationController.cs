using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnimationController : MonoBehaviour
{
    public AudioSource SFX;

    Animator _animator;

    public bool isAnimating { get; private set; }

    public delegate void StopHandler();
    public event StopHandler StopEvent;

    void Awake() {
        _animator = GetComponent<Animator>();
        isAnimating = true;
    }

    void OnStopPoint() {
        _animator.speed = 0f;
        isAnimating = false;

        if(StopEvent != null) {
            StopEvent();
        }
    }

    public void NextAnimation() {
        _animator.speed = 1f;
        isAnimating = true;
    }

    public void PlaySFX(AudioClip clip) {
        SFX.PlayOneShot(clip);
    }
}

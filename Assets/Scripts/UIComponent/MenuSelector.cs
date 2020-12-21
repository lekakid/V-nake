using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class MenuSelector : MonoBehaviour
{
    public RectTransform Selector;
    public Transform BtnGroup;

    public int Value { get; private set; }

    List<RectTransform> _menuPos;
    AudioSource _selectSFX;

    void Awake() {
        _menuPos = new List<RectTransform>();
        _selectSFX = GetComponent<AudioSource>();
        
        foreach(Transform child in BtnGroup) {
            _menuPos.Add(child.GetComponent<RectTransform>());
        }
    }

    public void Select(int index) {
        Selector.position = _menuPos[index].position;
        Value = index;
        _selectSFX.Play();
    }

    public void SelectPrev() {
        Value = (Value - 1 < 0) ? _menuPos.Count - 1 : Value - 1;
        Select(Value);
    }

    public void SelectNext() {
        Value = (Value + 1) % _menuPos.Count;
        Select(Value);
    }
}

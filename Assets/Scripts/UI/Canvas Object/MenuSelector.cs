using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSelector : MonoBehaviour
{
    public RectTransform Selector;

    public int Value { get; private set; }

    List<RectTransform> _menuPos;

    void Awake() {
        _menuPos = new List<RectTransform>();
        
        foreach(Transform child in transform) {
            _menuPos.Add(child.GetComponent<RectTransform>());
        }
    }

    public void Select(int index) {
        Selector.position = _menuPos[index].position;
        Value = index;
        SoundManager.Instance.PlaySFX("Select");
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

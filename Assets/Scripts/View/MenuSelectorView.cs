using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSelectorView : MonoView
{
    public RectTransform Selector;
    public GameObject[] Menus;

    public int selected { get; private set; }

    public void SetSelect(int index) {
        GameObject menu = Menus[index];
        Selector.position = menu.GetComponent<RectTransform>().position;
        EventSystem.current.SetSelectedGameObject(menu);
        selected = index;
        SoundManager.Instance.PlaySFX("Select");
    }

    public void SelectPrev() {
        selected = (selected - 1 < 0) ? Menus.Length - 1 : selected - 1;
        SetSelect(selected);
    }

    public void SelectNext() {
        selected = (selected + 1) % Menus.Length;
        SetSelect(selected);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : UIView
{
    [SerializeField] MenuSelector Selector = null;

    public int SelectorValue {
        get { return Selector.Value; }
    }

    public void SelectNext() {
        Selector.SelectNext();
    }

    public void SelectPrev() {
        Selector.SelectPrev();
    }
}

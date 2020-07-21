using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public UIView Current { 
        get {
            if(UIStack.Count == 0)
                return null;

            return UIStack.Peek();
        }
    }

    Stack<UIView> UIStack;

    void Awake() {
        if(Instance == null)
            Instance = this;
        else
            DestroyImmediate(this.gameObject);

        UIStack = new Stack<UIView>();
    }

    void OnApplicationQuit() {
        Instance = null;
    }

    public void Push(UIView view) {
        if(Current != null)
            Current.Hide();
        UIStack.Push(view);
        view.Show();
    }

    public void Pop() {
        if(Current == null)
            return;
        UIView v = UIStack.Pop();
        v.Hide();
        Current.Show();
    }

    public void Clear() {
        UIStack.Clear();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseView : MonoBehaviour
{
    [Header("Object")]
    public RectTransform Indexer;
    public GameObject[] Menus;

    int _selected;

    void Update()
    {
        if(GameManager.Instance.State != GameStateType.PAUSEMENU)
            return;
            
        if(Input.GetButtonDown("Cancel")) {
            GameManager.Instance.State = GameStateType.PLAY;
            return;
        }

        float y = Input.GetAxisRaw("Vertical");
        bool down = Input.GetButtonDown("Vertical");

        if(down && gameObject.activeSelf) {
            if(y < 0) {
                SetSelect(++_selected % Menus.Length);
            } else if (y > 0) {
                SetSelect((--_selected < 0) ? Menus.Length - 1 : _selected % Menus.Length);
            }
            return;
        }
    }

    public void SetSelect(int index) {
        if(!gameObject.activeSelf)
            return;

        Indexer.position = Menus[index].GetComponent<RectTransform>().position;
        EventSystem.current.SetSelectedGameObject(Menus[index]);
        _selected = index;
        SoundManager.Instance.PlaySFX("Select");
    }
}

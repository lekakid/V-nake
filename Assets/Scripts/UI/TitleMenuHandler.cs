using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleMenuHandler : MonoBehaviour
{
    [Header("Control")]
    public RectTransform Indexer;
    public GameObject[] Menus;

    [Header("View")]
    public GameObject ModalPanel;
    public GameObject HowToPopup;
    public GameObject SettingPopup;

    int _selected;

    void Start()
    {
        SoundManager.Instance.PlayBGM("Title");
    }

    void Update()
    {
        float y = Input.GetAxisRaw("Vertical");
        bool down = Input.GetButtonDown("Vertical");

        if(down && !ModalPanel.activeSelf) {
            if(y < 0) {
                SetSelect(++_selected % 5);
            } else if (y > 0) {
                SetSelect((--_selected < 0) ? 4 : _selected % 5);
            }
        }
    }

    public void SetSelect(int index) {
        if(ModalPanel.activeSelf)
            return;

        Indexer.position = Menus[index].GetComponent<RectTransform>().position;
        EventSystem.current.SetSelectedGameObject(Menus[index]);
        _selected = index;
    }

    public void Play() {
        SceneManager.LoadScene("Snake");
    }

    public void Howto() {
        EventSystem.current.SetSelectedGameObject(ModalPanel);
        ModalPanel.SetActive(true);
        HowToPopup.SetActive(true);
    }

    public void Gallery() {

    }

    public void Setting() {

    }

    public void Quit() {
        Application.Quit();
    }

    public void ModalOff() {
        ModalPanel.SetActive(false);
        HowToPopup.SetActive(false);
        EventSystem.current.SetSelectedGameObject(Menus[_selected]);
    }
}

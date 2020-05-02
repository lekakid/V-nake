using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [Header("Object")]
    public RectTransform Indexer;
    public GameObject[] Menus;

    [Header("View")]
    public GameObject ModalPanel;
    public GameObject HowToPopup;
    public GameObject SettingPopup;

    int _selected;

    void Start()
    {
        GameManager.Instance.CurrentManager = this.gameObject;
        SoundManager.Instance.PlayBGM("Title");
        GameManager.Instance.State = GameStateType.TITLE;
    }

    void Update()
    {
        if(GameManager.Instance.State != GameStateType.TITLE)
            return;

        float y = Input.GetAxisRaw("Vertical");
        bool down = Input.GetButtonDown("Vertical");

        if(down) {
            if(y < 0) {
                SetSelect(++_selected % Menus.Length);
            } else if (y > 0) {
                SetSelect((--_selected < 0) ? Menus.Length - 1 : _selected % Menus.Length);
            }
        }
    }

    public void SetSelect(int index) {
        if(ModalPanel.activeSelf)
            return;

        Indexer.position = Menus[index].GetComponent<RectTransform>().position;
        EventSystem.current.SetSelectedGameObject(Menus[index]);
        _selected = index;
        SoundManager.Instance.PlaySFX("Select");
    }

    public void Play() {
        SoundManager.Instance.PlaySFX("Select");
        SceneManager.LoadScene("Snake");
    }

    public void Howto() {
        EventSystem.current.SetSelectedGameObject(ModalPanel);
        ModalPanel.SetActive(true);
        HowToPopup.SetActive(true);
        SoundManager.Instance.PlaySFX("Select");
        GameManager.Instance.State = GameStateType.HOWTO;
    }

    public void Gallery() {
        SoundManager.Instance.PlaySFX("Select");
    }

    public void Setting() {
        SoundManager.Instance.PlaySFX("Select");
    }

    public void Quit() {
        SoundManager.Instance.PlaySFX("Select");
        Application.Quit();
    }

    public void ModalOff() {
        ModalPanel.SetActive(false);
        HowToPopup.SetActive(false);
        EventSystem.current.SetSelectedGameObject(Menus[_selected]);
        SoundManager.Instance.PlaySFX("Select");
        GameManager.Instance.State = GameStateType.TITLE;
    }
}

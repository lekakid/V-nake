using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public Text SpeakerName;
    public Text ContentScript;
    public CanvasView DialogueView;
    public CanvasGroup DialogueWrapper;
    public DialogueDatabase DialogueDatabase;
    public CharacterDatabase CharacterDatabase;

    Dialogue[] _currentDialogue;
    int _dialogueIndex = -1;
    DialogueAnimationController _dialogueAnimationController = null;
    GameObject _dialogueAnimation;

    void Update()
    {
        // 대화 진행 키보드 입력 처리
        if(Input.GetButtonDown("Submit") && _dialogueIndex > -1) {
            PrintNext();
            return;
        }

        // 대화 진행 마우스 입력 처리
        if(Input.GetMouseButtonUp(0) && _dialogueIndex > -1) {
            PrintNext();
            return;
        }
    }

    void OnEnable() {
        DialogueView.Show();
    }

    void OnDisable() {
        if(DialogueView)
            DialogueView.Hide();
    }

    public void RunDialogueScript(string key) {
        DialogueScriptableObject obj = DialogueDatabase.GetDialogue(key);
        if(obj == null)
            return;

        _currentDialogue = obj.Dialogues;
        _dialogueIndex = 0;

        if(obj.AnimationPrefab) {
            GameObject AnimationObject = Instantiate(obj.AnimationPrefab);
            RectTransform AnimationObejctRect = AnimationObject.GetComponent<RectTransform>();
            AnimationObject.transform.SetParent(transform);
            AnimationObject.transform.SetSiblingIndex(0);
            AnimationObejctRect.localPosition = Vector3.zero;
            AnimationObject.transform.localScale = Vector3.one;
            
            _dialogueAnimation = AnimationObject;
            _dialogueAnimationController = AnimationObject.GetComponent<DialogueAnimationController>();
            _dialogueAnimationController.StopEvent += ShowDialogue;
        }
        else {
            ShowDialogue();
        }
        PrintDialogue();
    }

    public void PrintNext() {
        SoundManager.Instance.PlaySFX("Select");
        if(_dialogueAnimationController) {
            // 애니메이션 진행 중 키 입력 무시
            if(_dialogueAnimationController.isAnimating)
                return;
            
            HideDialogue();
            _dialogueAnimationController.NextAnimation();
        }
        _dialogueIndex++;

        if(_dialogueIndex >= _currentDialogue.Length) {
            _dialogueIndex = -1;
            GameManager.UndoController();

            if(_dialogueAnimationController) {
                _dialogueAnimationController = null;
                Destroy(_dialogueAnimation);
            }
            return;
        }

        PrintDialogue();
    }

    public void PrintDialogue() {
        string name = _currentDialogue[_dialogueIndex].Name;
        string content = _currentDialogue[_dialogueIndex].Content;
        Match match;

        while(true) {
            match = Regex.Match(content, @"%.*%");
            if(match.Success) {
                string info = match.Value.Replace("%", "");
                string data;
                switch(info) {
                    case "Status.PlayTime":
                        float time = PlayerPrefs.GetFloat(info);
                        TimeSpan span = TimeSpan.FromSeconds(time);
                        data = span.ToString("hh':'mm");
                        break;
                    default:
                        data = PlayerPrefs.GetInt(info).ToString();
                        break;
                }
                content = content.Replace(match.Value, data);
            }
            else {
                break;
            }
        }

        SetName(name);
        SetContent(content);
    }

    public void ShowDialogue() {
        if(_currentDialogue[_dialogueIndex].Name != "Invisible")
            DialogueWrapper.alpha = 1f;
    }

    public void HideDialogue() {
        DialogueWrapper.alpha = 0f;
    }

    public void SetName(string name) {
        SpeakerName.text = name;
    }

    public void SetContent(string content) {
        ContentScript.text = content;
    }
}

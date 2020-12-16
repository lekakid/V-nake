using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public DialogueView DialogueView;
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

    public void RunDialogueScript(string key) {
        DialogueScriptableObject obj = DialogueDatabase.GetDialogue(key);
        if(obj == null)
            return;

        if(obj.AnimationPrefab) {
            GameObject AnimationObject = Instantiate(obj.AnimationPrefab);
            AnimationObject.transform.SetParent(transform);
            AnimationObject.transform.localScale = Vector3.one;
            
            _dialogueAnimation = AnimationObject;
            _dialogueAnimationController = AnimationObject.GetComponent<DialogueAnimationController>();
            _dialogueAnimationController.StopEvent += DialogueView.Show;
        }
        else {
            DialogueView.Show();
        }
        
        _currentDialogue = obj.Dialogues;
        _dialogueIndex = 0;
        PrintDialogue();
    }

    public void PrintNext() {
        SoundManager.Instance.PlaySFX("Select");
        if(_dialogueAnimationController) {
            // 애니메이션 진행 중
            if(_dialogueAnimationController.isAnimating) {
                _dialogueAnimationController.Skip();
                return;
            }
            DialogueView.Hide();
            _dialogueAnimationController.NextAnimation();
        }
        _dialogueIndex++;

        if(_dialogueIndex >= _currentDialogue.Length) {
            _dialogueIndex = -1;
            DialogueView.Hide();
            GameManager.UndoController();

            if(_dialogueAnimationController) {
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

        DialogueView.SetName(name);
        DialogueView.SetContent(content);
    }
}

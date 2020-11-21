using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public DialogueView DialogueView;
    public DialogueDatabase DialogueDatabase;

    Dialogue[] _currentDialogue;
    int _dialogueIndex = -1;

    void Awake() {
        GameManager.DialogueController = this;
    }

    void Update()
    {
        // 대화 진행 키보드 입력 처리
        if(Input.GetButtonUp("Submit") && _dialogueIndex > -1) {
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
        _currentDialogue = obj.Dialogues;
        _dialogueIndex = 0;

        UIManager.Instance.Push(DialogueView);

        PrintDialogue();
    }

    public void PrintNext() {
        _dialogueIndex++;

        if(_dialogueIndex >= _currentDialogue.Length) {
            _dialogueIndex = -1;
            UIManager.Instance.Pop();
            return;
        }

        PrintDialogue();
    }

    public void PrintDialogue() {
        DialogueView.SetName(_currentDialogue[_dialogueIndex].Name);
        DialogueView.SetContent(_currentDialogue[_dialogueIndex].Content);
    }
}

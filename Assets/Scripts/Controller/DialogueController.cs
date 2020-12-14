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
        
        _currentDialogue = obj.Dialogues;
        _dialogueIndex = 0;

        DialogueView.Show();

        PrintDialogue();
    }

    public void PrintNext() {
        _dialogueIndex++;

        if(_dialogueIndex >= _currentDialogue.Length) {
            _dialogueIndex = -1;
            DialogueView.Hide();
            GameManager.UndoController();
            return;
        }

        PrintDialogue();
    }

    public void PrintDialogue() {
        string name = _currentDialogue[_dialogueIndex].Name;
        string content = _currentDialogue[_dialogueIndex].Content;
        Match match;

        while(true) {
            match = Regex.Match(content, @"%Score\..*%");
            if(match.Success) {
                string info = match.Value.Replace("Score.", "");
                info = info.Replace("%", "");
                int score = CharacterDatabase.GetScore(info);
                content = content.Replace(match.Value, score.ToString());
            }
            else {
                break;
            }
        }

        DialogueView.SetName(name);
        DialogueView.SetContent(content);
    }
}

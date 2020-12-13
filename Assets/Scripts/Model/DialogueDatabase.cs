using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDatabase : MonoBehaviour
{
    Dictionary<string, DialogueScriptableObject> _db;

    string DialoguePath = "Dialogue";

    void Start() {
        DialogueScriptableObject[] list = Resources.LoadAll<DialogueScriptableObject>(DialoguePath);

        _db = new Dictionary<string, DialogueScriptableObject>();
        foreach(DialogueScriptableObject i in list) {
            _db.Add(i.name, i);
        }
    }

    public DialogueScriptableObject GetDialogue(string key) {
        DialogueScriptableObject result = null;
        _db.TryGetValue(key, out result);
        return result;
    }
}

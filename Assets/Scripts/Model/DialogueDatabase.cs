using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDatabase : MonoBehaviour
{
    [System.Serializable]
    public class Item {
        public string key;
        public DialogueScriptableObject value;
    }

    public Item[] DialogueList;
    Dictionary<string, DialogueScriptableObject> _db;

    void Start() {
        _db = new Dictionary<string, DialogueScriptableObject>();
        foreach(Item i in DialogueList) {
            _db.Add(i.key, i.value);
        }
    }

    public DialogueScriptableObject GetDialogue(string key) {
        return _db[key];
    }
}

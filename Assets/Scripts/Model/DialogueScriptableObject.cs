using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Data", menuName = "V-nake/Dialogues")]
public class DialogueScriptableObject : ScriptableObject
{
    public GameObject AnimationPrefab;
    public Dialogue[] Dialogues;
}

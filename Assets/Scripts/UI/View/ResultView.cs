using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultView : MenuView
{
    public Transform Grid;
    public GameObject CountPrefab;

    public void DrawResult(List<CharacterScriptableObject> list, Dictionary<string, int> dic) {
        foreach(Transform child in Grid) {
            Destroy(child.gameObject);
        }
        
        foreach(CharacterScriptableObject d in list) {
            if(dic[d.name] > 0) {
                GameObject i = Instantiate(CountPrefab);
                i.GetComponentInChildren<Image>().sprite = d.Image;
                i.GetComponentInChildren<TextMeshProUGUI>().SetText(string.Format("x{0:000}", dic[d.name]));
                i.transform.SetParent(Grid, false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultView : MenuView
{
    public Transform Grid;
    public GameObject CountPrefab;

    public void DrawResult() {
        CharacterScriptableObject[] list = Resources.LoadAll<CharacterScriptableObject>("Character");

        foreach(Transform child in Grid) {
            Destroy(child.gameObject);
        }
        
        foreach(CharacterScriptableObject d in list) {
            if(Status.Instance.CurrentCharacterRescueCounts[d.name] > 0) {
                GameObject i = Instantiate(CountPrefab);
                
                i.GetComponentInChildren<Image>().sprite = d.Image;
                string score = string.Format("x{0:000}", Status.Instance.CurrentCharacterRescueCounts[d.name]);
                i.GetComponentInChildren<TextMeshProUGUI>().SetText(score);
                i.transform.SetParent(Grid, false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultView : MonoView
{
    [Header("Result Item")]
    public Transform Grid;
    public GameObject CountPrefab;
    
    public new void SetActive(bool active) {
        if(active) {
            CharacterDatabase db = GameManager.Instance.SnakeController.CharacterDatabase;
            List<CharacterScriptableObject> list = db.Characters;
            Dictionary<string, int> dic = db.CurrentRescueScore;

            foreach(CharacterScriptableObject d in list) {
                if(dic[d.name] > 0) {
                    GameObject i = Instantiate(CountPrefab);
                    i.GetComponentInChildren<Image>().sprite = d.Image;
                    i.GetComponentInChildren<TextMeshProUGUI>().SetText(string.Format("x{0:000}", dic[d.name]));
                    i.transform.SetParent(Grid);
                }
            }
        }
        else {
            for(int i = Grid.childCount - 1; i >= 0; i--) {
                Destroy(Grid.GetChild(i).gameObject);
            }
        }

        gameObject.SetActive(active);
    }
}

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

    public void Init() {
        for(int i = Grid.childCount - 1; i >= 0; i--) {
            Destroy(Grid.GetChild(i).gameObject);
        }
    }
    
    public new void SetActive(bool active) {
        if(active) {
            SpawnController spawner = GameManager.Instance.SpawnController;
            List<Character> list = spawner.CharacterList;
            Dictionary<Character, int> dic = spawner.RescueCount;

            foreach(Character c in list) {
                if(dic[c] > 0) {
                    GameObject i = Instantiate(CountPrefab);
                    i.GetComponentInChildren<Image>().sprite = c.SpriteRenderer.sprite;
                    i.GetComponentInChildren<TextMeshProUGUI>().SetText(string.Format("x{0:00}", dic[c]));
                    i.transform.SetParent(Grid);
                }
            }
        }

        gameObject.SetActive(active);
    }
}

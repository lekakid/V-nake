using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Object")]
    public SnakeController Controller;
    public CharacterSpawner Spawner;
    public GameObject Bush;
    public Tilemap Land;

    [Header("Field")]
    public float Width;
    public float Height;

    [Header("UI")]
    public GameObject PauseView;
    public ResultView ResultView;
    
    void Start()
    {
        Controller.PlaySnake();
    }

    void Update()
    {
        if(Input.GetButtonDown("Cancel")) {
            Pause();
        }
    }

    public void MoveBush() {
        Vector2 pos = new Vector2();

        do {
            float x = Mathf.Round(Random.Range(0, Width));
            float y = Mathf.Round(Random.Range(0, Height));

            pos.x = x;
            pos.y = y;
        } while(Controller.ExistTail(pos));
        
        Bush.transform.position = pos;
    }

    public void Pause() {
        if(ResultView.gameObject.activeSelf)
            return;
            
        PauseView.SetActive(!PauseView.activeSelf);

        if(PauseView.activeSelf) {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }
    }

    public void Restart() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Snake");
    }

    public void ReturnMenu() {
        // Time.timeScale = 1;
        //EditorSceneManager.LoadScene("Mainmenu");
    }

    public void ShowResult() {
        List<Character> list = Spawner.CharacterList;
        Dictionary<Character, int> dic = Spawner.SpawnCount;
        
        if(dic.Count > 0) {
            foreach(Character c in list) {
                if(dic.ContainsKey(c)) {
                    ResultView.addItem(c.SpriteRenderer.sprite, dic[c]);
                }
            }
        }

        ResultView.Show();
    }
}

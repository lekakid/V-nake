using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Object")]
    public SnakeController Controller;
    public CharacterManager CharacterManager;
    public GameObject Bush;
    public Tilemap Land;

    [Header("Border")]
    public float Left;
    public float Right;
    public float Top;
    public float Bottom;

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
        float x = Mathf.Round(Random.Range(Left, Right));
        float y = Mathf.Round(Random.Range(Bottom, Top));
        
        Bush.transform.position = new Vector2(x, y);
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
        EditorSceneManager.LoadScene("Snake");
    }

    public void ReturnMenu() {
        EditorSceneManager.LoadScene("Mainmenu");
    }

    public void ShowResult() {
        List<Character> list = CharacterManager.CharacterList;
        Dictionary<Character, int> dic = CharacterManager.CharacterCount;
        
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

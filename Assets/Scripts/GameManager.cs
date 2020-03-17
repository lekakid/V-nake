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
    public GameObject PauseMenu;
    public GameObject ResultView;
    public GameObject ResultGrid;
    public GameObject ResultItemPrefab;
    
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
        PauseMenu.SetActive(!PauseMenu.activeSelf);

        if(PauseMenu.activeSelf) {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }
    }

    public void Restart() {
        Time.timeScale = 1;
        EditorSceneManager.LoadScene("Snake");
    }

    public void ReturnMenu() {
        Time.timeScale = 1;
        EditorSceneManager.LoadScene("Mainmenu");
    }

    public void ShowResult() {
        Time.timeScale = 0;

        List<Character> list = CharacterManager.CharacterList;
        Dictionary<Character, int> dic = CharacterManager.CharacterCount;
        
        if(dic.Count > 0) {
            foreach(Character c in list) {
                if(dic.ContainsKey(c)) {
                    ResultItem i = Instantiate(ResultItemPrefab.gameObject).GetComponent<ResultItem>();

                    i.img.sprite = c.sprite.sprite;
                    i.txt.text = string.Format("x{0:00}", dic[c]);
                    i.transform.parent = ResultGrid.transform;
                }
            }
        }

        ResultView.SetActive(true);
    }
}

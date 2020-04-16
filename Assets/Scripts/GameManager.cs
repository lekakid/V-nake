using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Object")]
    public SnakeController SnakeController;

    [Header("View")]
    public GameObject PauseView;
    public ResultView ResultView;

    public static GameManager Instance {
        get { return _instance; }
    }

    public CharacterSpawner CharacterSpawner {
        get { return _characterSpawner; }
    }

    static GameManager _instance;
    CharacterSpawner _characterSpawner;

    void Awake()
    {
        if(!_instance) {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            DestroyImmediate(this.gameObject);
        }

        _characterSpawner = GetComponent<CharacterSpawner>();
    }
    
    void Start()
    {
        SnakeController.PlaySnake();
    }

    void Update()
    {
        if(Input.GetButtonDown("Cancel")) {
            Pause();
        }
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
        //Time.timeScale = 1;
        //DestoryImmediate(this.gameObject);
        //EditorSceneManager.LoadScene("Mainmenu");
    }

    public void ShowResult() {
        List<Character> list = _characterSpawner.CharacterList;
        Dictionary<Character, int> dic = _characterSpawner.SpawnCount;
        
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

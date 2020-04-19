using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    static GameManager _instance;

    void Awake()
    {
        _instance = this;
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

    public void GameOver() {
        ResultView.gameObject.SetActive(true);
        ResultView.Show();
    }
}

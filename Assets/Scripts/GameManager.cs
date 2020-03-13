using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SnakeController Controller;
    public CharacterManager CharacterManager;

    public GameObject Bush;
    public Tilemap Land;

    public float Left, Right;
    public float Top, Bottom;

    public GameObject PauseMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        Controller.PlaySnake();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
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

    public void ShowResult() {
        
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
}

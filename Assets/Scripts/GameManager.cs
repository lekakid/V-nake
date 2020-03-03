using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public SnakeController Controller;

    public GameObject Bush;
    public Tilemap Land;

    public float Left, Right;
    public float Top, Bottom;

    public Text tmp;
    
    // Start is called before the first frame update
    void Start()
    {
        Controller.PlayGame();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MoveBush() {
        float x = Mathf.Round(Random.Range(Left, Right));
        float y = Mathf.Round(Random.Range(Bottom, Top));
        
        Bush.transform.position = new Vector2(x, y);
    }

    public void ShowResult() {
        
    }
}

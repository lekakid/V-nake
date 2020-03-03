using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public GameObject bush;
    public Tilemap Land;

    public float Left, Right;
    public float Top, Bottom;

    public Text tmp;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MoveBush() {
        float x = Mathf.Round(Random.Range(Left, Right));
        float y = Mathf.Round(Random.Range(Bottom, Top));
        
        bush.transform.position = new Vector2(x, y);
    }

    public void Play() {
        
    }

    public void ShowResult() {
        
    }
}

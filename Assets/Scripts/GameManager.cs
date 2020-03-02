using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Character LastCharacter;
    public GameObject bush;
    public Transform Land;

    public Text tmp;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown) {
            tmp.enabled = false;
            Time.timeScale = 1;
        }
    }

    void MoveBush() {

    }
}

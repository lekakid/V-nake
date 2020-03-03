using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Head : Character
{
    public SnakeController Controller;
    public GameManager Manager;

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bush")) {
            // Getting Character Event
            Controller.AddTail();
            Manager.MoveBush();
        }

        if(other.CompareTag("Block")) {
            // Game Over
            Controller.StopGame();
        }
    }
}

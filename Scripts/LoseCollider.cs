using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    

    GameSession theGameSession;
    Ball theBall;

    private void Start()
    {
        theGameSession = FindObjectOfType<GameSession>();
        theBall = FindObjectOfType<Ball>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (theGameSession.CurrentLives() > 0)
        {
            theGameSession.SubFromLives();
            // relies on a public var in Ball.cs... can I find a better way?
            theBall.hasStarted = false;
            theBall.LockBalltoPaddle();
            theBall.LaunchOnMouseClick();
        }
        else
        {
            SceneManager.LoadScene("Game Over");
        }
        
    }

}

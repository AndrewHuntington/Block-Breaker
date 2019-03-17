using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //configuration parameters
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;

    // cache references
    Ball theBall;
    GameSession theGameSession;

    // Start is called before the first frame update
    void Start()
    {
        theBall = FindObjectOfType<Ball>();
        theGameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        //how to find the position of your mouse on the X axis using world units
        //Debug.Log(Input.mousePosition.x / Screen.width * screenWidthInUnits);
        

        //moves the paddle along the X axis w/the mouse
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX); //keeps the paddle from going off screen on the X axis
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (theGameSession.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x; // allows for autoplay
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits; // user input
        }
    }
}

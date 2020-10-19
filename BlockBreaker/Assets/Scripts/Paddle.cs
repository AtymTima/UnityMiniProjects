using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //config params
    float gameWidth;
    float mousePos;
    float convertToGameUnits;
    [SerializeField] readonly float minX;
    float maxX;

    //cashed reference
    AutoPlay autoPlay;
    Ball ball;

    void Start()
    {
        DetermineScreenSize();
        autoPlay = FindObjectOfType<AutoPlay>();
        ball = FindObjectOfType<Ball>();
    }

    private void DetermineScreenSize()
    {
        float height = Camera.main.orthographicSize * 2.0f;
        gameWidth = height * Screen.width / Screen.height;
        maxX = gameWidth - 2.0f;
        convertToGameUnits = gameWidth / Screen.width;
    }

    void Update()
    {
        FollowMousePosition();
    }

    private void FollowMousePosition()
    {
        Vector2 paddlePos = new Vector2(GetXPos(), transform.position.y)
        {
            x = Mathf.Clamp(GetXPos() - 1.0f, minX, maxX)
        };
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (autoPlay.IsAutoPlayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x * convertToGameUnits;
        }
    }
}

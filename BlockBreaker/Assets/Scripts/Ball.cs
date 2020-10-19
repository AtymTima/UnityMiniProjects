using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    //config params
    [SerializeField] Paddle paddle1;
    [SerializeField] float launchX;
    [SerializeField] float launchY;
    [SerializeField] float upVelocityFactor = 0.2f;
    [SerializeField] AudioClip[] audioClips;

    //cashed references
    Vector2 paddleToBallVector;
    AudioSource audioSource;
    Rigidbody2D myRigidBody2D;

    //state params
    bool hasStarted = false;

    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        audioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();

    }

    void Update()
    {

        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnClick(launchX, launchY);
        }

    }

    private void LaunchOnClick(float X, float Y)
    {
        if (Input.GetMouseButtonDown(0))
        {
            myRigidBody2D.velocity = new Vector2(X, Y);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 randomTweak = new Vector2(
        UnityEngine.Random.Range(0, upVelocityFactor), 
        UnityEngine.Random.Range(0, upVelocityFactor));

        if (hasStarted)
        {
            PlayBlockAudioOnce();
            myRigidBody2D.velocity += randomTweak;
        }
    }

    private void PlayBlockAudioOnce()
    {
        AudioClip clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
        audioSource.PlayOneShot(clip);
    }
}

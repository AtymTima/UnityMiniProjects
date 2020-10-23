using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Cashed References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private InputAction WASD;
    [SerializeField] private Animator playerAnim;
    private bool isRunning;
    private bool hasChanged;
    [Header("Movement Parameters")]
    [SerializeField] private float movementSpeed = 5f;
    private Vector2 movementVector;
    private Vector2 zeroVector = new Vector2(0, 0);
    private Vector2 flipScale = new Vector2(0,1);
    private float movementXPos;

    private void OnEnable() { WASD.Enable(); }
    private void OnDisable() { WASD.Disable(); }

    private void Update()
    {
        movementVector = WASD.ReadValue<Vector2>();
        movementXPos = movementVector.x;
        if (movementVector != zeroVector)
        {
            switch(!hasChanged)
            {
                case true:
                    hasChanged = true;
                    isRunning = true;
                    playerAnim.SetBool("isRunning", isRunning);
                    break;
            }
            flipScale.x = Mathf.Sign(movementXPos);
            playerTransform.localScale = flipScale;
        }
        else
        {
            switch (hasChanged)
            {
                case true:
                    hasChanged = false;
                    isRunning = false;
                    playerAnim.SetBool("isRunning", isRunning);
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        switch(isRunning)
        {
            case true:
                playerRB.velocity = movementVector * movementSpeed * Time.fixedDeltaTime;
                break;
            case false:
                movementVector.x = 0;
                movementVector.y = 0;
                playerRB.velocity = movementVector;
                break;
        }
    }
}

using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private float speedMovement = 5;
    private Vector3 rightDirection = new Vector3(0, 0, 0);
    private Vector3 leftDirection = new Vector3(0, 180, 0);
    private float currentDirection;
    private Vector2 movementVector;
    private bool isRunning;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform feetTransform;
    private Collider2D[] groundCollisions = new Collider2D[1];
    private Vector2 jumpVector;
    private int groundCount;
    private bool isGrounded;
    private bool hasJump;
    private bool hasLanded;
    private bool fallsDown = true;

    [Header("Jump Timer")]
    [SerializeField] private float timeForJumping = 0.2f;
    private float currentTimeInAir;
    private float gravityScaleUp = 5;
    private float gravityScaleDown = 10;

    [SerializeField] private PlayerAnimator playerAnimator;
    private KeyCode jumpKey = KeyCode.Space;
    private KeyCode leftKey = KeyCode.A;
    private KeyCode rightKey = KeyCode.D;
    private KeyCode skipDialogueKey = KeyCode.C;
    private KeyCode quitGame = KeyCode.P;

    private bool isBlocked;

    private void Update()
    {
        switch (isBlocked)
        {
            case true:
                if (Input.GetKeyDown(skipDialogueKey))
                {
                    //DialogueManager.dialogueManagerInstance.DisplayNextSentence();
                }
                return;
        }
        groundCount = Physics2D.OverlapCircleNonAlloc(feetTransform.position, 0.05f, groundCollisions, whatIsGround);
        switch (groundCount) { case 0: break; 
        default: isGrounded = true; break; }
        if (isGrounded && fallsDown)
        {
            fallsDown = false;
            playerAnimator.LandAnimation();
        }

        CheckIfRunning();
        CheckIfJumping();
        CheckIfQuit();
    }

    private void CheckIfRunning()
    {
        if (Input.GetKeyDown(rightKey))
        {
            isRunning = true;
            playerAnimator.RunAnimation(isRunning);
            playerTransform.eulerAngles = rightDirection;
            currentDirection = 1;
        }
        if (Input.GetKeyDown(leftKey))
        {
            isRunning = true;
            playerAnimator.RunAnimation(isRunning);

            playerTransform.eulerAngles = leftDirection;
            currentDirection = -1;
        }
        if (isRunning && Mathf.Abs(Input.GetAxisRaw("Horizontal")) < Mathf.Epsilon)
        {
            isRunning = false;
            playerAnimator.RunAnimation(isRunning);
        }
    }

    private void CheckIfJumping()
    {
        if (isGrounded && Input.GetKeyDown(jumpKey))
        {
            hasJump = true;
            currentTimeInAir = timeForJumping;
            playerRB.gravityScale = gravityScaleUp;
            playerAnimator.JumpAnimation();

        }
        if (hasJump && (Input.GetKeyUp(jumpKey) || TimerStopJump()))
        {
            FallDown();
        }
    }

    private void CheckIfQuit()
    {
        if (Input.GetKeyDown(quitGame)) { Application.Quit(); }
    }

    private bool TimerStopJump()
    {
        if (currentTimeInAir > 0)
        {
            currentTimeInAir -= Time.deltaTime;
            return false;
        }
        return true;
    }

    private void FixedUpdate()
    {
        switch(isRunning)
        {
            case true:
                movementVector.x = currentDirection * speedMovement * Time.fixedDeltaTime;
                movementVector.y = playerRB.velocity.y;
                playerRB.velocity = movementVector;
                break;
        }
        switch (hasJump)
        {
            case true:
                isGrounded = false;
                jumpVector.x = playerRB.velocity.x;
                jumpVector.y = jumpForce;
                playerRB.velocity = jumpVector;
                break;
        }
    }

    private void FallDown()
    {
        hasJump = false;
        fallsDown = true;
        playerRB.gravityScale = gravityScaleDown;
    }

    private void BlockInput(bool blocked)
    {
        isBlocked = blocked;
        switch(blocked) { case true: FallDown(); break; }
    }
}

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
    [Header("Spites")]
    [SerializeField] private SpriteRenderer[] spriteRenderersBody;
    [SerializeField] private Transform gunTransform;
    [Header("Movement Parameters")]
    [SerializeField] private float movementSpeed = 5f;
    private Vector2 movementVector;
    private Vector2 zeroVector = new Vector2(0, 0);
    private Vector2 flipScale = new Vector2(1,1);

    private float movementXPos;
    [Header("Role Parameters")]
    [SerializeField] private bool isImposter;
    [SerializeField] private bool hasControl;
    [SerializeField] private Color32 myColor;
    [SerializeField] private InputAction KILL;
    private bool isDead;

    private void OnEnable() { WASD.Enable(); KILL.Enable(); }
    private void OnDisable() { WASD.Disable(); KILL.Disable(); }

    private void Awake()
    {
        for (int i = 0; i < spriteRenderersBody.Length; i++)
        {
            spriteRenderersBody[i].color = myColor;
        }
        if(playerTransform.localScale.x < 0)
        {
            Debug.Log("1");
            flipScale.x = -1;
        }
    }

    private void Update()
    {
        if(!hasControl) { return; }
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
            switch(isImposter) { case false: gunTransform.localScale = flipScale; break; }
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
        if (!hasControl) { return; }
        switch (isRunning)
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

    public float GetCurrentFlip() { return flipScale.x; }
    public bool GetIfHasControl() { return hasControl; }
    public void SetNoControl() { hasControl = false; }
}

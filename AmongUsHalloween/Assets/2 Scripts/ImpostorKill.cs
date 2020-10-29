using UnityEngine;
using UnityEngine.InputSystem;

public class ImpostorKill : MonoBehaviour
{
    [Header("Cash References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerSounds playerSounds;
    [SerializeField] private Transform impostorTransform;
    [SerializeField] private Animator impostorAnim;
    private Vector2 targetPos;

    [Header("Positions and Visibility")]
    [SerializeField] private LayerMask visibleLayer;
    [SerializeField] private LayerMask invisibleLayer;
    [SerializeField] private GameObject[] bodyChildren;
    private float currentSide;
    private Vector2 leftPosition = new Vector2(-1.5f, 0);
    private Vector2 rightPosition = new Vector2(1.5f, 0);
    private Vector2 killPosition;

    [Header("Input Actions")]
    [SerializeField] private InputAction KILL;
    PlayerMovement currentPlayerMove;
    private void OnEnable() { KILL.Enable(); }
    private void OnDisable() { KILL.Disable(); }

    private void Awake()
    {
        if(!playerMovement.GetIfHasControl()) { return;  }
        KILL.performed += context => KillPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            currentPlayerMove = collision.GetComponent<PlayerMovement>();
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        if (currentPlayerMove != null)
        {
            impostorAnim.SetTrigger("isKill");
            MakeVisible();
            playerSounds.PlayHitSound();
            playerSounds.PlayImpactSound();
            currentPlayerMove.gameObject.GetComponent<PlayerMovement>().SetNoControl();
            currentPlayerMove.gameObject.GetComponent<Animator>().SetTrigger("isDead");
            currentSide = currentPlayerMove.GetCurrentFlip();
            if (currentSide > 0)
            {
                killPosition = leftPosition;
            }
            else
            {
                killPosition = rightPosition;
            }
            targetPos = currentPlayerMove.transform.position;
            targetPos.x += killPosition.x;
            targetPos.y += killPosition.y;
            impostorTransform.localScale = currentPlayerMove.transform.localScale;
            impostorTransform.localPosition = targetPos;
        }
    }

    private void MakeVisible()
    {
        ChangeVisibility("Default");
    }

    public void MakeInvisible()
    {
        ChangeVisibility("Hidden");
    }

    public void ChangeVisibility(string layerName)
    {
        for (int i = 0; i < bodyChildren.Length; i++)
        {
            bodyChildren[i].layer = LayerMask.NameToLayer(layerName);
        }
    }
}

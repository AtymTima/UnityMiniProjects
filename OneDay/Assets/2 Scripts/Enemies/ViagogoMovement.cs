using UnityEngine;

public class ViagogoMovement : MonoBehaviour
{
    [SerializeField] private Transform viagogoTransform;
    [SerializeField] private Transform targetPlayer;
    [SerializeField] private Animator viagogoAnim;
    [SerializeField] private float speedOfFollow = 10f;
    private float initialSpeed;
    [SerializeField] private AudioSource hitSource;
    [SerializeField] private AudioSource damagedSource;
    [SerializeField] private GameObject closedDoor;

    private string playerTag = "Player";
    public Dialogues[] dialogues;
    private Vector2 targetVector;
    private Vector2 viagogoVector;

    private int signDirection = 1;
    private int remainingLives = 2;

    private void Awake()
    {
        initialSpeed = speedOfFollow;
    }

    private void Update()
    {
        targetVector = targetPlayer.localPosition;
        targetVector.y += 2.8f;
        viagogoTransform.localPosition = Vector2.MoveTowards(viagogoTransform.localPosition,
        targetVector, speedOfFollow * signDirection * Time.deltaTime);

        viagogoVector = viagogoTransform.localPosition;
        viagogoVector.y = Mathf.Clamp(viagogoVector.y, -4, 8);
        viagogoVector.x = Mathf.Clamp(viagogoVector.x, 455, 480);

        viagogoTransform.localPosition = viagogoVector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(playerTag)) { return; }
        switch (signDirection)
        {
            case 1:
                targetPlayer.gameObject.GetComponent<OreoDisplayer>().SubstractScore();
                hitSource.Play();
                break;
            case -1:
                targetPlayer.gameObject.GetComponent<OreoDisplayer>().RenewScoreAfterHit();
                ReverseDirection();
                damagedSource.Play();
                break;
        }
    }

    public void ReverseDirection()
    {
        switch (signDirection)
        {
            case 1:
                viagogoAnim.SetTrigger("isRun");
                speedOfFollow = initialSpeed * 5;
                break;
            case -1:

                switch(remainingLives)
                {
                    case 0:
                        viagogoAnim.SetTrigger("isDead");
                        speedOfFollow = initialSpeed;
                        closedDoor.SetActive(false);
                        DialogueManager.dialogueManagerInstance.StartDialogue(dialogues);
                        break;
                    default:
                        viagogoAnim.SetTrigger("isHit");
                        speedOfFollow = initialSpeed;
                        remainingLives--;
                        break;
                }
                break;
        }
        signDirection *= -1;
    }

    public void OnDead()
    {
        gameObject.SetActive(false);
    }
}

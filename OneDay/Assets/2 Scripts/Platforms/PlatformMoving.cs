using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    [SerializeField] private Transform platformTransform;
    [SerializeField] private bool isHorizontal = true;
    [SerializeField] private float speedOfMoving = 10f;
    [SerializeField] private SpriteRenderer mySprite;
    [SerializeField] private bool isFlippable;
    private Vector3 currentDirection;
    private Vector3 rightDirection = new Vector2(1f, 0);
    private Vector3 upDirection = new Vector2(0, 1f);
    private string platformTag = "PlatformMoving";

    private void Awake()
    {
        switch (isHorizontal) { case true: currentDirection = rightDirection * speedOfMoving; break; 
                                case false: currentDirection = upDirection * speedOfMoving; break; }
    }

    private void Update()
    {
        platformTransform.localPosition += currentDirection * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.CompareTag(platformTag))
        {
            case true:
                switch(isFlippable)
                {
                    case true:
                        mySprite.flipX = !mySprite.flipX;
                        break;
                }
                currentDirection *= -1;
                break;
        }
    }
}

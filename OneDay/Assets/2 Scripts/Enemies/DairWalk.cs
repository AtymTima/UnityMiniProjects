using UnityEngine;

public class DairWalk : MonoBehaviour
{
    [SerializeField] private Rigidbody2D dairRB;
    [SerializeField] private float speedMovement = 3f;
    private Vector2 movementVector;

    private void FixedUpdate()
    {
        movementVector.x = speedMovement * Time.fixedDeltaTime;
        movementVector.y = dairRB.velocity.y;
        dairRB.velocity = movementVector;
    }
}

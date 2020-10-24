using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Cash References")]
    [SerializeField] private Transform cameraTransform;
    [Header("Parameters")]
    [SerializeField] private float moveSpeed = 5f;
    private Transform targetPlayer;
    private Vector3 currentPos;
    private Vector3 velocity;

    private void Awake()
    {
        targetPlayer = cameraTransform.parent.GetComponentInChildren<PlayerMovement>().transform;
        currentPos.z = -1; 
    }

    void Update()
    {
        currentPos = new Vector3(targetPlayer.position.x, targetPlayer.position.y, transform.position.z);
        velocity = (currentPos - cameraTransform.position) * 4f;
        transform.position = Vector3.SmoothDamp(cameraTransform.position, currentPos, 
        ref velocity, 1.0f, moveSpeed * Time.deltaTime);
    }
}

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Cash References")]
    [SerializeField] private Transform cameraTransform;
    private Transform targetPlayer;
    private Vector3 currentPos;

    private void Awake()
    {
        targetPlayer = cameraTransform.parent.GetComponentInChildren<PlayerMovement>().transform;
        currentPos.z = -1;
    }

    private void Update()
    {
        currentPos.x = targetPlayer.localPosition.x;
        currentPos.y = targetPlayer.localPosition.y;
        cameraTransform.localPosition = currentPos;
    }
}

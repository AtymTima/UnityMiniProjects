using UnityEngine;

public class CursorFollower : MonoBehaviour
{
    [SerializeField] private Transform myTransform;
    [SerializeField] private Camera mainCamera;
    private Vector3 currentPos;

    private void Awake()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        currentPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        currentPos.z = 11;
        myTransform.localPosition = currentPos;
    }
}

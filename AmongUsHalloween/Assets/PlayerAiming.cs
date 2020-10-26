using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAiming : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private InputAction mouseClick;

    private void OnEnable() { mouseClick.Enable(); }
    private void OnDisable() { mouseClick.Disable(); }

    private void Awake()
    {
        mouseClick.performed += context => fieldOfView.SetFieldOfViewParams();

    }

    private void Update()
    {
        fieldOfView.SetOriginPos(playerTransform.position);

        Vector3 screenPosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        worldPosition -= playerTransform.position;
        fieldOfView.SetAimingAngle(worldPosition);


        //if (mouseClick.performed)
        //{
        //    fieldOfView.SetFieldOfViewParams();
        //}
    }
}

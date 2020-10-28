using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAiming : MonoBehaviour
{
    [Header("Cashed References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform playerGun;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private FieldOfView fieldOfView;
    [Header("Input Actions")]
    [SerializeField] private InputAction mouseClick;
    private Vector3 gunAngleVector;

    public Quaternion GunAngleVector 
    { 
        get
        {
            return Quaternion.Euler(0, 0, gunAngleVector.z - fieldOfView.CurrentFieldOfView / 2);
        }
        private set
        {
            gunAngleVector = value.eulerAngles;
        }
    }

    private void OnEnable() { mouseClick.Enable(); }
    private void OnDisable() { mouseClick.Disable(); }

    private void Awake()
    {
        mouseClick.performed += context => fieldOfView.SetFieldOfViewParams();
    }

    private void Update()
    {
        ChangeFieldOfViewAngle();
        ChangeGunPosition();
    }

    private void ChangeFieldOfViewAngle()
    {
        fieldOfView.SetOriginPos(playerTransform.position);
        Vector3 screenPosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        worldPosition -= playerTransform.position;
        fieldOfView.SetAimingAngle(worldPosition);
    }

    private void ChangeGunPosition()
    {
        float gunAngle = fieldOfView.StartingAngle;
        gunAngleVector.z = gunAngle;
        playerGun.rotation = Quaternion.Euler(0, 0, gunAngleVector.z - fieldOfView.CurrentFieldOfView / 2);
    }
}

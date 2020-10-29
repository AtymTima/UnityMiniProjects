using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAiming : MonoBehaviour
{
    [Header("Cashed References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerMovement playerMovement;
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
            return Quaternion.Euler(0, 0, gunAngleVector.z - fieldOfView.CurrentFieldOfView / 2 - 15);
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
        ChangeFieldOfViewAngle();
    }

    private void Start()
    {
        mouseClick.performed += context => fieldOfView.SetFieldOfViewParams();
        if (!playerMovement.GetIfHasControl())
        {
            ChangeFieldOfViewAngle();
            enabled = false;
        }
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

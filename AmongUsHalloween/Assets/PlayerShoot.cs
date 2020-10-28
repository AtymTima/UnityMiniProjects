using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [Header("Cashed References")]
    [SerializeField] private Transform playerGun;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform effectParent;
    [SerializeField] private PlayerAiming playerAiming;
    [Header("Input Actions")]
    [SerializeField] private InputAction DAMAGE;
    [SerializeField] private LayerMask ignoreLayer;

    private ObjectPool<FireShoot> fireShootPool;
    private FireShoot fireShoot;
    private ObjectPool<HitRed> hitRedPool;
    private HitRed hitRed;

    private void OnEnable() { DAMAGE.Enable(); }
    private void OnDisable() { DAMAGE.Disable(); }

    private void Awake()
    {
        DAMAGE.performed += ctx => FireRaycast();
    }

    private void Start()
    {
        fireShootPool = ObjectPool<FireShoot>.poolInstance;
        hitRedPool = ObjectPool<HitRed>.poolInstance;
    }

    private void FireRaycast()
    {
        SpawnHitEffect();
        SpawnFireShoot();
        CheckIfHitTarget();
    }

    private void SpawnHitEffect()
    {
        RaycastHit2D firstEncounterRey = Physics2D.Raycast(playerGun.position, playerGun.right,
                Mathf.Infinity, ~ignoreLayer);
        hitRed = hitRedPool.GetObject();
        hitRed.transform.position = firstEncounterRey.point;
        hitRed.transform.localRotation = playerAiming.GunAngleVector;
        hitRed.transform.parent = effectParent;
        hitRed.gameObject.SetActive(true);
    }

    private void SpawnFireShoot()
    {
        fireShoot = fireShootPool.GetObject();
        fireShoot.transform.position = shootingPoint.position;
        fireShoot.transform.localRotation = playerAiming.GunAngleVector;
        fireShoot.transform.parent = effectParent;
        fireShoot.gameObject.SetActive(true);
    }

    private void CheckIfHitTarget()
    {
        RaycastHit2D solidRay = Physics2D.Raycast(playerGun.position, playerGun.right,
                Mathf.Infinity, 1 << LayerMask.NameToLayer("Solid"));
        RaycastHit2D hiddenRay = Physics2D.Raycast(playerGun.position, playerGun.right,
        Mathf.Infinity, 1 << LayerMask.NameToLayer("Hidden"));
        if (Mathf.Abs(solidRay.distance) < Mathf.Abs(hiddenRay.distance)
        || !hiddenRay)
        {
            return;
        }
        hiddenRay.collider.GetComponent<PlayerDeath>().IsKilled();
    }
}

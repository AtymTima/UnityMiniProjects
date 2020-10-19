using UnityEngine;

public class AudioCollision : MonoBehaviour
{
    [SerializeField] private Transform audioTransform;
    [SerializeField] private BoxCollider2D audioCollider;
    [SerializeField] private AudioSource currentSource;
    [SerializeField] private float maxDistanceOfSound = 30;
    private Transform targetObject;
    private float maxVolume;

    private void Awake()
    {
        maxVolume = currentSource.volume;
        currentSource.enabled = false;
        audioCollider.size = new Vector2(maxDistanceOfSound, audioCollider.size.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        targetObject = collision.gameObject.transform;
        currentSource.volume = 0;
        currentSource.enabled = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        currentSource.volume = maxVolume - DistanceBetweenObjects() / maxDistanceOfSound;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentSource.enabled = false;
    }

    private float DistanceBetweenObjects()
    {
        return Mathf.Abs(audioTransform.localPosition.x - targetObject.transform.localPosition.x) * 2 * maxVolume;
    }
}

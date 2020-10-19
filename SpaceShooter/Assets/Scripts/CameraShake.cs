using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 originalPos;

    private void Awake()
    {
        originalPos = transform.localPosition;
    }

    public IEnumerator ShakeCamera(float durationShake, float magnitudeShake)
    {
        float elapsed = 0f;

        while (elapsed < durationShake)
        {
            float randomPosX = Random.Range(-1f, 1f) * magnitudeShake;
            float randomPosY = Random.Range(-1f, 1f) * magnitudeShake;

            transform.position = new Vector3(originalPos.x + randomPosX, originalPos.y + randomPosY, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}

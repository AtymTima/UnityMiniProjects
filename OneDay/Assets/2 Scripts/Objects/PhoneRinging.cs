using UnityEngine;

public class PhoneRinging : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform backgroundTransform;
    [SerializeField] private AudioSource phoneSource;
    [SerializeField] private const float defaultShakeDuration = 0.2f;
    [SerializeField] private const float defaultShakePower = 0.25f;
    [SerializeField] private const float defaultShakeSpeed = 10f;
    public bool IsShaking { get; private set; }
    private float currentDuration;
    private float currentPower;
    private float currentSpeed;
    private Vector3 initialTransformCamera;
    private Vector3 initialTransformBackground;
    private Vector3 nextPosition;

    private void LateUpdate()
    {
        switch (IsShaking)
        {
            case true:
                if (currentDuration > 0)
                {
                    ChangeCameraPosition();
                    currentDuration -= Time.deltaTime;
                }
                else
                {
                    StopShaking();
                }
                break;
        }
    }

    private void ChangeCameraPosition()
    {
        cameraTransform.localPosition = Vector3.MoveTowards(cameraTransform.localPosition, nextPosition, currentSpeed * Time.deltaTime);
        backgroundTransform.localPosition = Vector3.MoveTowards(backgroundTransform.localPosition, nextPosition, currentSpeed * Time.deltaTime);

        if (cameraTransform.localPosition == nextPosition)
        {
            nextPosition.x = initialTransformCamera.x + Random.Range(-currentPower, currentPower);
            nextPosition.y = initialTransformCamera.y + Random.Range(-currentPower, currentPower);
        }
    }

    private void StopShaking()
    {
        IsShaking = false;
        cameraTransform.localPosition = initialTransformCamera;
        backgroundTransform.localPosition = initialTransformBackground;
    }

    public void ShakeSurroundings(int isFirstVibration)
    {
        switch(isFirstVibration)
        {
            case 1:
                phoneSource.Play();
                break;
        }
        IsShaking = true;
        currentDuration = defaultShakeDuration;
        currentPower = defaultShakePower;
        currentSpeed = defaultShakeSpeed;
        initialTransformCamera = cameraTransform.localPosition;
        initialTransformBackground = backgroundTransform.localPosition;
        nextPosition = initialTransformCamera;
    }
}

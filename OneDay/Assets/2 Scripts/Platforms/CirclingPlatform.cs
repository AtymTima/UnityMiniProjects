using UnityEngine;

public class CirclingPlatform : MonoBehaviour
{
    [SerializeField] private Transform centerOfCircle;
    [SerializeField] private Transform platformCenter;
    [SerializeField] private float speedOfMoving = 4f;
    [SerializeField] private bool isClockWise;
    private float radiusOfCircle;
    private float centerPointX;
    private float centerPointY;
    private float angle;
    private float currentXPos;
    private float currentYPos;
    private Vector2 currentPos;

    private void Awake()
    {
        radiusOfCircle = GetRadiusPiffagor(centerOfCircle, platformCenter);
        centerPointX = centerOfCircle.localPosition.x;
        centerPointY = centerOfCircle.localPosition.y;
        speedOfMoving = isClockWise ? -speedOfMoving : speedOfMoving;
    }

    private float GetRadiusPiffagor(Transform center, Transform platform)
    {
        float xDistance = centerOfCircle.localPosition.x - platformCenter.position.x;
        float yDistance = centerOfCircle.localPosition.y - platformCenter.position.y;
        return Mathf.Sqrt(xDistance * xDistance + yDistance * yDistance);
    }

    private void Update()
    {
        currentXPos = centerPointX + Mathf.Cos(angle) * radiusOfCircle;
        currentYPos = centerPointY + Mathf.Sin(angle) * radiusOfCircle;
        currentPos.x = currentXPos;
        currentPos.y = currentYPos;
        platformCenter.position = currentPos;
        angle += Time.deltaTime * speedOfMoving;
        if (angle > 360) { angle = 0; }
    }
}

using UnityEngine;

public class ScreenSizeConverter : MonoBehaviour
{
    public float ConvertToGameUnits()
    {
        float height = Camera.main.orthographicSize * 2.0f;
        var gameWidth = height * Screen.width / Screen.height;
        return gameWidth / Screen.width;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLost : MonoBehaviour
{
    [SerializeField] GameController GameController;

    public delegate void OnLevelLost(bool restart);
    public static event OnLevelLost onLevelLost = delegate { };

    public void OnRestartClick()
    {
        GameController?.ResetGameLevel();
        onLevelLost?.Invoke(true);
    }

    public void OnReturnToMenuClick()
    {
        GameController?.ResetGameLevel();
        onLevelLost?.Invoke(false);
    }
}

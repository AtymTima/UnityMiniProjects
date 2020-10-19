using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoader : MonoBehaviour
{

    void Awake()
    {
        SetUpSingletone();
    }

    private void SetUpSingletone()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}

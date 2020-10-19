using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int totalNumberOfScenes = SceneManager.sceneCountInBuildSettings - 1;
        SceneManager.LoadScene(totalNumberOfScenes);
    }
}

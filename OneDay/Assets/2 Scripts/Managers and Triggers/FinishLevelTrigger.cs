using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevelTrigger : MonoBehaviour
{
    [SerializeField] private Animator fadeAnim;
    [SerializeField] private GameObject[] childObjects;
    [SerializeField] private bool isFadeNeeded;
    private string isFadeOut = "isFadeOut";

    private void Awake()
    {
        for (int i=0; i< childObjects.Length; i++)
        {
            childObjects[i].SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(isFadeNeeded)
        {
            case true:
                fadeAnim.SetTrigger(isFadeOut);
                break;
            case false:
                OnFadeOut(0);
                break;
        }
    }

    public void OnFadeOut(int sceneNumber)
    {
        if (gameObject.scene.buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
            PlayerPrefs.SetInt("Level", 1);
        }
        else
        {
            SceneManager.LoadScene(gameObject.scene.buildIndex + 1);
            PlayerPrefs.SetInt("Level", gameObject.scene.buildIndex + 1);

        }
    }
}

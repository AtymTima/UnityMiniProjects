using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;
    [SerializeField] private GameObject[] decorations;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private GameObject[] platforms;
    private int currentLevelPart;

    private void Awake()
    {
        for (int i=2; i < characters.Length; i++)
        {
            ChangeStateOfParts(i, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnloadPreviousParts();
    }

    private void UnloadPreviousParts()
    {
        ChangeStateOfParts(currentLevelPart, false);
        currentLevelPart += 2;
        if (currentLevelPart >= characters.Length) { return; }
        LoadNextParts();
    }

    private void LoadNextParts()
    {
        ChangeStateOfParts(currentLevelPart, true);
        currentLevelPart--;
    }

    private void ChangeStateOfParts(int index, bool state)
    {
        characters[index].SetActive(state);
        decorations[index].SetActive(state);
        obstacles[index].SetActive(state);
        platforms[index].SetActive(state);
    }
}

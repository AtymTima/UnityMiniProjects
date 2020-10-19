using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] lastLevelObjects;

    public void EnableLastLevel()
    {
        for (int i=0; i < lastLevelObjects.Length; i++)
        {
            lastLevelObjects[i].SetActive(true);
        }
    }
}

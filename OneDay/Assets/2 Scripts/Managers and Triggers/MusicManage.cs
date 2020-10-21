using UnityEngine;

public class MusicManage : MonoBehaviour
{
    [SerializeField] private AudioClip bossFight;
    [SerializeField] private AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopCurrentMusic();
    }

    private void StopCurrentMusic()
    {
        audioSource.Stop();
    }

    public void StartBossFight()
    {
        audioSource.clip = bossFight;
        audioSource.Play();
    }
}

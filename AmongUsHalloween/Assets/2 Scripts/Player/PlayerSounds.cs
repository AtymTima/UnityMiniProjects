using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private Sounds[] sounds;
    private int lastStepSFX;

    private void Awake()
    {
        CreateAudioSources();
    }

    private void CreateAudioSources()
    {
        for (int i=0; i < sounds.Length; i++)
        {
            if (sounds[i].soundSource == null)
            {
                sounds[i].soundSource = gameObject.AddComponent<AudioSource>();
                sounds[i].soundSource.clip = sounds[i].soundClip;
                sounds[i].soundSource.volume = sounds[i].soundVolume;
            }
        }
    }

    public void PlayHitSound()
    {
        sounds[0].soundSource.Play();
    }

    public void PlayImpactSound()
    {
        sounds[1].soundSource.Play();
    }

    public void PlayReloadSound()
    {
        sounds[2].soundSource.Play();
    }

    public void PlayRunningSound()
    {
        int randomIndex = 0;
        do
        {
            randomIndex = Random.Range(3, 6);
        } while (randomIndex == lastStepSFX);
        sounds[randomIndex].soundSource.Play();
    }
}

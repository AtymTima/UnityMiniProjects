using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private Sounds[] sounds;

    private void Awake()
    {
        CreateAudioSources();
    }

    private void CreateAudioSources()
    {
        for (int i=0; i < sounds.Length; i++)
        {
            sounds[i].soundSource = gameObject.AddComponent<AudioSource>();
            sounds[i].soundSource.clip = sounds[i].soundClip;
            sounds[i].soundSource.volume = sounds[i].soundVolume;
            sounds[i].soundSource.loop = sounds[i].isLoop;
        }
    }

    public void RunningSound(bool isRunning)
    {
        switch (isRunning)
        {
            case true:
                sounds[0].soundSource.Play();
                break;
            case false:
                sounds[0].soundSource.Stop();
                break;
        }
    }

    public void JumpSound()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].soundSource.Stop();
        }
        sounds[1].soundSource.Play();
    }

    public void LandSound()
    {
        sounds[2].soundSource.Play();
    }

    public void EatSound()
    {
        sounds[3].soundSource.Play();
    }

    public void PhotoSound()
    {
        sounds[4].soundSource.Play();
    }

    public void StopAllSounds()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].soundSource.Stop();
        }
    }
}

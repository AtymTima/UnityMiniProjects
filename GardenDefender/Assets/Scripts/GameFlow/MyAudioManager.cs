using UnityEngine;
using System;

public class MyAudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static MyAudioManager instance;

    void Awake()
    {
        SetUpSingleton();

        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = sound.audioVolume;
            sound.audioSource.pitch = sound.audioPitch;
            sound.audioSource.loop = sound.loop;
        }
    }

    #region Singleton
    private void SetUpSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        //if (FindObjectsOfType(GetType()).Length > 1)
        //{
        //    gameObject.SetActive(false);
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    DontDestroyOnLoad(gameObject);
        //}
    }
    #endregion

    public void PlayAudio(string audioName, bool custom, float audioVolume, float audioPitch)
    {

        Sound currentSound = Array.Find(sounds, soundFX => soundFX.audioName == audioName);

        switch (custom)
        {
            case false:
                break;
            case true:
                currentSound.audioSource.volume = audioVolume;
                currentSound.audioSource.pitch = audioPitch;
                break;
        }

        if (currentSound == null) return;

        currentSound.audioSource.Play();
    }

    public void StopAudio(string audioName)
    {
        Sound currentSound = Array.Find(sounds, soundFX => soundFX.audioName == audioName);
        if (currentSound == null) return;

        currentSound.audioSource.Stop();
    }

}

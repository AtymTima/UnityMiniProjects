using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip audioClip;
    public string audioName;
    [Space]
    [Range(0f, 1f)]
    public float audioVolume = 0.5f;
    [Range(0.1f, 3f)]
    public float audioPitch = 1f;
    public bool loop;

    [HideInInspector]
    public AudioSource audioSource;
}

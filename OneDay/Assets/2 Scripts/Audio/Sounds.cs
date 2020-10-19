using UnityEngine;

[System.Serializable]
public class Sounds
{
    [HideInInspector] public AudioSource soundSource;
    public AudioClip soundClip;
    [Range(0, 1)] public float soundVolume = 0.4f;
    public bool isLoop;
}

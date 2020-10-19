using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVFX : MonoBehaviour
{
    public void PlaySound(AudioClip sound, float volume)
    {
        var cameraPos2D = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
        AudioSource.PlayClipAtPoint(sound, cameraPos2D, volume);
    }
}

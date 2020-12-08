using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Permanent SFX")]
    [SerializeField] private AudioSource clickSource;
    [SerializeField] private AudioSource eventSource;
    [SerializeField] private AudioSource purchaseSource;
    [SerializeField] private AudioClip explosionSFX;

    [Header("Level-dependant SFX")]
    [SerializeField] private AudioSource ambienceSource;
    [SerializeField] private AudioSource mainMusic;
    [SerializeField] private AudioClip fireworksSFX;
    [SerializeField] private AudioClip congratsSong;

    [Header("Variations of click sound")]
    [SerializeField] private AudioClip[] clickSounds;
    private int lastPlayedClickSFX;
    private int maxLength;

    private void Awake()
    {
        maxLength = clickSounds.Length;
    }

    public void PlayClickSFX()
    {
        int newIndex = 0;
        do
        {
            newIndex = Random.Range(0, maxLength - 1);
        } while (newIndex == lastPlayedClickSFX);
        lastPlayedClickSFX = newIndex;
        clickSource.PlayOneShot(clickSounds[newIndex]);
        clickSource.panStereo = Random.Range(-0.2f, 0.2f);
    }

    public void PlayGoodEventSFX()
    {
        if (!eventSource.isPlaying)
        {
            eventSource.Play();
        }
    }

    public void PlayPurchaseSFX()
    {
        purchaseSource.Play();
    }

    public void PlayExplosionSFX()
    {
        clickSource.volume = 0.3f;
        clickSource.PlayOneShot(explosionSFX);
    }

    public void ChangeSounds()
    {
        ambienceSource.clip = fireworksSFX;
        ambienceSource.volume = 0.1f;
        mainMusic.clip = congratsSong;
        ambienceSource.Play();
        mainMusic.Play();
    }
}

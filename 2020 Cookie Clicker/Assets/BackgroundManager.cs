using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private Animator backgroundAnim;
    [SerializeField] private Animation fireworkAnimation;
    [SerializeField] private string isDead = "isDead";
    [SerializeField] private SoundManager soundManager;

    private void Awake()
    {
        DavidAnim.OnDavidDead += OnEnd;
    }

    private void OnDestroy()
    {
        DavidAnim.OnDavidDead -= OnEnd;
    }

    private void OnEnd()
    {
        backgroundAnim.SetTrigger(isDead);
        backgroundAnim.speed = 0.4f;
        soundManager.ChangeSounds();
    }
}

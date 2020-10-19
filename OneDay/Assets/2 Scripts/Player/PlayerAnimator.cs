using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;
    [SerializeField] private SoundPlayer soundPlayer;
    private string isRunning = "isRunning";
    private string isJumping = "isJumping";
    private string isLanding = "isLanding";

    private void Awake()
    {
        JumpAnimation();
    }

    public void RunAnimation(bool isState)
    {
        SetAnimation(isRunning, isState);
        soundPlayer.RunningSound(isState);
    }
    public void JumpAnimation()
    {
        playerAnim.ResetTrigger(isLanding);
        SetAnimation(isJumping);
        soundPlayer.JumpSound();
    }
    public void LandAnimation()
    {
        SetAnimation(isLanding);
        soundPlayer.LandSound();
    }

    private void SetAnimation(string currentAnim, bool currentState)
    {
        playerAnim.SetBool(currentAnim, currentState);
    }
    private void SetAnimation(string currentAnim)
    {
        playerAnim.SetTrigger(currentAnim);
    }
}

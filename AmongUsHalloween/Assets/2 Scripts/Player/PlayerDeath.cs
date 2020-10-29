using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [Header("Cashed References")]
    [SerializeField] private Animator playerAnim;
    [Header("Parameters")]
    [SerializeField] private string isDead = "isDead";

    public void IsKilled()
    {
        playerAnim.SetBool(isDead, true);
    }
}

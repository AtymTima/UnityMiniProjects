using UnityEngine;

public class EggCollider : MonoBehaviour
{
    [SerializeField] private Animator eggAnim;
    [SerializeField] private string Ground = "Ground";
    [SerializeField] private string isBroken = "isBroken";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.CompareTag(Ground))
        {
            case true:
                eggAnim.SetTrigger(isBroken);
                break;
        }
    }
}

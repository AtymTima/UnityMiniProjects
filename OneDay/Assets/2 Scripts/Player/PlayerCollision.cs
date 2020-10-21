using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private SoundPlayer soundPlayer;
    //[SerializeField] private OreoDisplayer oreoDisplayer;
    [SerializeField] private string oreoTag = "Oreo";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.CompareTag(oreoTag))
        {
            case true:
                collision.GetComponent<Animator>().SetTrigger("isHit");
                //oreoDisplayer.UpdateScore();
                soundPlayer.EatSound();
                break;
        }
    }
}

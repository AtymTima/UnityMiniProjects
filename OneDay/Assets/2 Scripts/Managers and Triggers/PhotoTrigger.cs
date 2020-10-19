using UnityEngine;

public class PhotoTrigger : MonoBehaviour
{
    [SerializeField] private Animator photoAnim;
    public Dialogues[] dialogues;
    private bool isDialogueShown;
    private string isPhotoRevealed = "isPhotoRevealed";

    private GameObject target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<OreoDisplayer>()?.CurrentScore >= 5)
        {
            switch (isDialogueShown)
            {
                case false:
                    photoAnim.SetTrigger(isPhotoRevealed);
                    target = collision.gameObject;
                    collision.GetComponent<OreoDisplayer>().ResetScore();
                    collision.GetComponent<SoundPlayer>().PhotoSound();
                    break;
            }
        }
        else
        {
            return;
        }
    }

    public void TriggerDialogue()
    {
        target.GetComponent<SoundPlayer>().StopAllSounds();
        DialogueManager.dialogueManagerInstance.StartDialogue(dialogues);
        isDialogueShown = true;
    }
}

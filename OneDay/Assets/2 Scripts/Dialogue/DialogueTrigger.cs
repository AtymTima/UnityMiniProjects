using UnityEngine;
using System;

public class DialogueTrigger : MonoBehaviour
{
    public static Action OnBossActivate;
    public Dialogues[] dialogues;
    private bool isDialogueShown;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(isDialogueShown)
        {
            case false:
                TriggerDialogue();
                collision.GetComponent<SoundPlayer>().StopAllSounds();
                if(gameObject.GetComponent<BossTrigger>() != null)
                {
                    gameObject.GetComponent<BossTrigger>().EnableLastLevel();
                    OnBossActivate?.Invoke();
                }
                break;
        }
    }

    private void TriggerDialogue()
    {
        DialogueManager.dialogueManagerInstance.StartDialogue(dialogues);
        isDialogueShown = true;
    }
}

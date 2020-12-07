using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private ScoreManager scoreManager;
    private int[] thresholds = { 50, 150, 300, 500, 750, 1100, 1500, 2100, 3000, 4000, 6000, 9500, 
    15000, 24500, 45000, 75000, 100000, 150000, 250000, 500000, 600000, 700000, 800000, 900000, 1000000};
    int dialogueCounter;
    int total;

    private void Awake()
    {
        PlayerInteractions.OnCookieClicked += CheckIfNextDialogue;
        total = thresholds.Length;
    }

    private void OnDestroy()
    {
        PlayerInteractions.OnCookieClicked -= CheckIfNextDialogue;
    }

    private void CheckIfNextDialogue()
    {
        if (scoreManager.currentScore+1 >= thresholds[dialogueCounter])
        {
            bool loop = true;
            while(loop)
            {
                if (dialogueCounter+1 <= total && scoreManager.currentScore >= thresholds[dialogueCounter+1])
                {
                    dialogueCounter++;
                }
                else
                {
                    loop = false;
                }
            }
            TriggerDialogue();
            dialogueCounter++;
        }
    }

    private void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogueCounter);
    }
}

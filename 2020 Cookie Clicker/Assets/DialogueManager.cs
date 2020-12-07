using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("Cashed References")]
    [SerializeField] private Dialogues[] allDialogues;
    [SerializeField] private Animator dialogueAnim;
    [SerializeField] private string isOpen = "isOpen";

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI sentenceText;
    [SerializeField] private Image avaImage;
    private Dialogues currentDialogue;
    private Coroutine currentCoroutine;
    private Queue<string> newDialogues;
    private int lastShown;
    private int lastToBeShown;
    private bool isDialogue;

    private void Awake()
    {
        newDialogues = new Queue<string>();
    }

    public void StartDialogue(int counter)
    {
        dialogueAnim.SetBool(isOpen, true);
        lastToBeShown = counter;
        isDialogue = true;
        ChooseDialogue();
    }

    private void ChooseDialogue()
    {
        currentDialogue = allDialogues[lastShown];
        titleText.text = currentDialogue.title;
        avaImage.sprite = currentDialogue.ava;
        DisplayNextDialogue();
    }

    private void DisplayNextDialogue()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(DisplayCharactersOfText(currentDialogue.description));
    }

    private IEnumerator DisplayCharactersOfText(string sentence)
    {
        sentenceText.text = "";
        for (int i = 0; i < sentence.Length; i++)
        {
            sentenceText.text += sentence[i];
            yield return null;
        }
    }

    public void EndDialogue()
    {
        if(!isDialogue) { return; }
        if (lastShown >= lastToBeShown)
        {
            Time.timeScale = 1;
            dialogueAnim.SetBool(isOpen, false);
            isDialogue = false;
            lastShown++;
            return;
        }
        ChooseDialogue();
        lastShown++;
    }
}

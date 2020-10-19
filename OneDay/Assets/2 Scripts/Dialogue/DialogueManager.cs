using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    static public DialogueManager dialogueManagerInstance;
    static public Action<bool> OnDialogueBox;

    private Queue<string> sentences;
    private string currentSentence;
    private string isOpen = "isOpen";
    private Dialogues[] allDialogues;
    private Dialogues currentDialogue;
    private int currentIndex;
    private int maxIndex;

    [SerializeField] private Animator dialogueAnim;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI sentenceText;
    [SerializeField] private Image avaImage;

    private void Awake()
    {
        sentences = new Queue<string>();
        dialogueManagerInstance = this;
    }

    public void StartDialogue(Dialogues[] dialogues)
    {
        allDialogues = dialogues;
        maxIndex = dialogues.Length;
        DisplayDialogue();
    }

    private void DisplayDialogue()
    {
        currentDialogue = allDialogues[currentIndex];
        OnDialogueBox.Invoke(true);
        nameText.text = currentDialogue.name;
        avaImage.sprite = currentDialogue.currentAva;
        dialogueAnim.SetBool(isOpen, true);
        sentences.Clear();

        for (int i = 0; i < currentDialogue.sentences.Length; i++)
        {
            sentences.Enqueue(currentDialogue.sentences[i]);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        currentSentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(DisplayCharactersOfText(currentSentence));
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

    private void EndDialogue()
    {
        if (currentIndex < maxIndex - 1)
        {
            currentIndex++;
            DisplayDialogue();
        }
        else
        {
            Time.timeScale = 1;
            dialogueAnim.SetBool(isOpen, false);
            OnDialogueBox.Invoke(false);
            currentIndex = 0;
        }
    }

    public void StopTimeScale()
    {
        Time.timeScale = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialoguePanel;
    public Transform player;

    [Header("Dialogue Settings")]
    [SerializeField] private float typingSpeed;
    [SerializeField] private float delayBetweenSentences;

    [Header("Starting Dialogue")]
    [SerializeField]
    [TextArea(3, 10)]
    private string[] startingDialogue = new string[]
    {
        "Greetings, adventurers! Your journey is about to begin!",
        "In this adventure, you will face many challenges.",
        "One of the most important tasks is to jump and hit red blocks to complete quests, unlocking the key to progress through the next levels.",
        "Remember, you only have 5 hearts.",
        "Along the way, you may encounter adorable-looking creatures in the forest, but beware! Some may be venomous and can reduce your hearts�or worse, lead to your demise.",
        "Fight back by stomping on them or avoid confrontation whenever possible.",
        "Additionally, there are countless traps scattered along the path, testing your skills and courage.",
        "Now, prepare yourself and enjoy the adventure ahead! Good luck!"
    };

    private Queue<string> sentences;
    // private bool isTyping = false;

    private static bool hasDialoguePlayed = false;

    private void Start()
    {
        sentences = new Queue<string>();

        if (!hasDialoguePlayed)
        {

            hasDialoguePlayed = true; 
            PauseGame();
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        sentences.Clear();

        foreach (string sentence in startingDialogue)
        {
            sentences.Enqueue(sentence);
        }

        StartCoroutine(AutoDisplayDialogue());
    }

    IEnumerator AutoDisplayDialogue()
    {
        while (sentences.Count > 0)
        {
            string sentence = sentences.Dequeue();
            yield return StartCoroutine(TypeSentence(sentence));
            yield return new WaitForSecondsRealtime(delayBetweenSentences);
        }

        EndDialogue();
    }

    IEnumerator TypeSentence(string sentence)
    {
        // isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        // isTyping = false;
    }

    private void EndDialogue()
    {
        dialogueText.text = ""; 
        dialoguePanel.SetActive(false);
        ResumeGame(); 
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}

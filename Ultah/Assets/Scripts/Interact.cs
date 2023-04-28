using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interact : MonoBehaviour
{
    public Dialogue dialogue;

    public GameObject dialogueView;
    public TextMeshProUGUI dialogueText;

    Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        dialogueView.SetActive(true);

        sentences = new Queue<string>();

        StartCoroutine(WaitStartDialogue(dialogue));
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // jika kalimat habis
        if (sentences.Count == 0)
        {
            StartDialogue(dialogue);
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            // durasi menampilkan huruf-huruf
            yield return new WaitForSeconds(0.15f);
        }

        // durasi untuk jeda ganti kalimat
        yield return new WaitForSeconds(1f);

        DisplayNextSentence();
    }

    IEnumerator WaitStartDialogue(Dialogue dialogue)
    {
        yield return new WaitForSeconds(2f);
        StartDialogue(dialogue);
    }
}

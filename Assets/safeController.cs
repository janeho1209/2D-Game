using UnityEngine;

public class SafeController : MonoBehaviour
{
    public int[] correctCombo = { 5,4,3,4 };
    int index = 0;
    public Sprite openSafe;
    private SpriteRenderer spriteRenderer;
    private Dialogue dialogue;
    public Dialogue.DialogueLine[] triggerDialogue;
    private AudioSource audioSource;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        dialogue = FindObjectOfType<Dialogue>();
        GameObject audioGO = GameObject.Find("open");
        if (audioGO != null)
        {
            audioSource = audioGO.GetComponent<AudioSource>();
        }

    }

    public void UserInput(int inputNumber)
    {
        if (inputNumber == correctCombo[index])
        {
            index++; //if correct number is inputted, move up counter

            if (index >= correctCombo.Length) //correct sequence inputed opens safe
            {
                if (audioSource != null && audioSource.clip != null)
                {
                    audioSource.Play();
                }
                spriteRenderer.sprite = openSafe;
                dialogue.StartDialogue(triggerDialogue);
            }
        }
        else //wrong number in sequence
        {
            index = 0;
        }
    }
}

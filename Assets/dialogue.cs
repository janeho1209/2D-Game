using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [System.Serializable]
    public struct DialogueLine //each struct should have these three things
    {
        public string characterName;
        public string line;
        public Sprite characterSprite;
    }

    public DialogueLine[] dialogueLines;
    public TMP_Text dialogueText;
    public TMP_Text nameText;
    public GameObject dialogueBox;
    public Image characterImage;
    private int currentLine = 0;
    private bool dialogueActive = false;

    void Start()
    {
        if (dialogueLines.Length > 0) //immediately start game with dialogue
        {
            StartDialogue();
        }
    }

    void Update()
    {
        if (!dialogueActive) return;

        if (Input.GetMouseButtonDown(0)) //each click brings in next line
        {
            NextLine();
        }
    }

    public void StartDialogue(DialogueLine[] newDialogueLines = null)
    {
        if (newDialogueLines != null) //to be or not to be
        {
            dialogueLines = newDialogueLines;
        }

        if (dialogueLines == null || dialogueLines.Length == 0)
        {
            return;
        }

        dialogueActive = true;
        dialogueBox.SetActive(true);
        currentLine = 0;
        ShowLine();
    }

    void ShowLine()
    {
        DialogueLine line = dialogueLines[currentLine]; //going thru the lines
        dialogueText.text = line.line;
        nameText.text = line.characterName;

        if (line.characterSprite != null) //displaying the character sprite
        {
            characterImage.sprite = line.characterSprite;
            characterImage.gameObject.SetActive(true);
        }
    }

    void NextLine()
    {
        currentLine++;
        if (currentLine < dialogueLines.Length)
        {
            ShowLine();
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        characterImage.gameObject.SetActive(false);
        dialogueActive = false;

        FinishButtonHandler finishButton = FindObjectOfType<FinishButtonHandler>(); //wanted to make finish appear and reappear depending on dialogue playing, couldn't figure it out...
        finishButton.ShowButton();
    }
}

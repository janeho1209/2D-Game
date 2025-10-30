using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [System.Serializable]
    public struct DialogueLine
    {
        public string characterName;
        [TextArea(2, 5)] public string line;
        public Sprite characterSprite; // portrait for each line
    }

    public DialogueLine[] dialogueLines;

    [Header("UI References")]
    public TMP_Text dialogueText;
    public TMP_Text nameText;
    public GameObject dialogueBox;
    public Image characterImage; // assign UI Image for portrait

    private int currentLine = 0;
    private bool dialogueActive = false;

    void Start()
    {
        // Start sequence automatically for now
        if (dialogueLines.Length > 0)
        {
            StartDialogue();
        }
    }

    void Update()
    {
        if (!dialogueActive) return;

        if (Input.GetMouseButtonDown(0)) // click to advance
        {
            NextLine();
        }
    }

    public void StartDialogue(DialogueLine[] newDialogueLines = null)
    {
        if (newDialogueLines != null)
            dialogueLines = newDialogueLines;

        if (dialogueLines == null || dialogueLines.Length == 0)
            return;

        dialogueActive = true;
        dialogueBox.SetActive(true);
        currentLine = 0;
        ShowLine();
    }

    void ShowLine()
    {
        DialogueLine line = dialogueLines[currentLine];
        dialogueText.text = line.line;
        nameText.text = line.characterName;

        if (line.characterSprite != null)
        {
            characterImage.sprite = line.characterSprite;
            characterImage.gameObject.SetActive(true);
        }
        else
        {
            characterImage.gameObject.SetActive(false);
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

        FinishButtonHandler finishButton = FindObjectOfType<FinishButtonHandler>();
        if (finishButton != null)
            finishButton.ShowButton();
    }
}

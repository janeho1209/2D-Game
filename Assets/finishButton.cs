using UnityEngine;
using UnityEngine.UI;

public class FinishButtonHandler : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public Dialogue dialogueManager;             // Assign your Dialogue object in Inspector
    public Dialogue.DialogueLine[] nextDialogue; // Dialogue to start when button is clicked
    public GameObject optionBoxes;
    public GameObject quizzing;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    // Call this from Dialogue when initial sequence ends
    public void ShowButton()
    {
        gameObject.SetActive(true);
    }


    public void OnFinishButtonClicked()
    {
        // Hide the Finish button
        gameObject.SetActive(false);

        // Trigger the next dialogue (if set)
        dialogueManager.StartDialogue(nextDialogue);
        quizzing.SetActive(true);

        // Optional: enable player control or other interactions
        // GameManager.Instance.EnablePlayerControl();
    }
}

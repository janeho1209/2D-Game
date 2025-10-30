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
        if (button != null)
            button.onClick.AddListener(OnFinishButtonClicked);
    }

    // Call this from Dialogue when initial sequence ends
    public void ShowButton()
    {
        gameObject.SetActive(true);
        Debug.Log("Finish button activated: " + gameObject.activeSelf);
    }


    private void OnFinishButtonClicked()
    {
        // Hide the Finish button
        gameObject.SetActive(false);

        // Trigger the next dialogue (if set)
        if (dialogueManager != null && nextDialogue != null && nextDialogue.Length > 0)
        {
            dialogueManager.StartDialogue(nextDialogue);
        }


        quizzing.SetActive(true);
        optionBoxes.SetActive(true);
        Debug.Log("Option boxes are now visible!");

        // Optional: enable player control or other interactions
        // GameManager.Instance.EnablePlayerControl();
    }
}

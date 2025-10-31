using UnityEngine;
using UnityEngine.UI;

public class FinishButtonHandler : MonoBehaviour
{
    public Dialogue dialogueManager;
    public Dialogue.DialogueLine[] nextDialogue;
    public GameObject optionBoxes;
    public GameObject quizzing;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }
    public void ShowButton() //wanted to make it appear reappear, doesn't work :/
    {
        gameObject.SetActive(true);
    }


    public void OnFinishButtonClicked()
    {
        gameObject.SetActive(false); //makes it disappear when clicked

        dialogueManager.StartDialogue(nextDialogue); //bring in dialogue
        quizzing.SetActive(true); //start the quiz segment (theoretically...)
    }
}

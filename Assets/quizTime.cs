using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueQuizHandler : MonoBehaviour
{
    [Header("UI References")]
    public Button[] quizOptions;          // Assign 3 buttons in Inspector
    public TMP_Text[] quizButtonTexts;    // Assign their text fields (matching order)
    public Dialogue dialogue;             // Reference your Dialogue manager
    public GameObject options;

    [System.Serializable]
    public struct QuizLine
    {
        public string questionCharacterName;
        [TextArea(2, 5)] public string questionText;
        public Sprite questionCharacterSprite;
        public string[] options;           // Exactly 3 choices (or however many quizOptions)
        public int correctOptionIndex;     // Index (0..N-1)

        public Dialogue.DialogueLine correctResponse;   // Dialogue if correct
        public Dialogue.DialogueLine incorrectResponse; // Dialogue if wrong
    }

    [Header("Quiz Data")]
    public QuizLine[] quizQuestions;

    private int score = 0;
    private int current = 0;

    public void StartQuiz()
    {
        score = 0;
        current = 0;
        ShowNextQuestion();
        options.SetActive(true);
    }

    private void ShowNextQuestion()
    {
        if (quizQuestions == null || quizQuestions.Length == 0)
        {
            EndQuiz();
            return;
        }

        if (current >= quizQuestions.Length)
        {
            EndQuiz();
            return;
        }

        QuizLine q = quizQuestions[current];

        // Display question via Dialogue UI (we're writing directly to Dialogue fields)
        dialogue.dialogueText.text = q.questionText;
        dialogue.nameText.text = q.questionCharacterName;
        if (dialogue.characterImage != null)
        {
            dialogue.characterImage.sprite = q.questionCharacterSprite;
            dialogue.characterImage.gameObject.SetActive(q.questionCharacterSprite != null);
        }
        if (dialogue.dialogueBox != null) dialogue.dialogueBox.SetActive(true);

        // Safety: ensure we have enough buttons/texts
        int optionCount = (q.options != null) ? q.options.Length : 0;
        for (int i = 0; i < quizOptions.Length; i++)
        {
            if (i < optionCount)
            {
                quizOptions[i].gameObject.SetActive(true);
                quizButtonTexts[i].text = q.options[i];

                int index = i; // capture
                quizOptions[i].onClick.RemoveAllListeners();
                quizOptions[i].onClick.AddListener(() => OnAnswerSelected(index));
            }
            else
            {
                quizOptions[i].gameObject.SetActive(false);
            }
        }

        // disable advancing the normal dialogue while question is shown
        //dialogue.DialoguePause(true);
    }

    private void OnAnswerSelected(int selectedIndex)
    {
        QuizLine q = quizQuestions[current];

        // Hide quiz buttons
        foreach (var b in quizOptions) b.gameObject.SetActive(false);

        // Check answer and play response via Dialogue manager
        if (selectedIndex == q.correctOptionIndex)
        {
            score++;
            // Play the correctResponse as a short dialogue (array with single line)
            dialogue.StartDialogue(new Dialogue.DialogueLine[] { q.correctResponse });
        }
        else
        {
            dialogue.StartDialogue(new Dialogue.DialogueLine[] { q.incorrectResponse });
        }

        current++;
        // resume normal dialogue progression after the short response
        //dialogue.DialoguePause(false);
    }

    private void EndQuiz()
    {
        Debug.Log($"Quiz ended! Score: {score}/{quizQuestions.Length}");

        // Example: final feedback
        if (score == quizQuestions.Length)
        {
            dialogue.StartDialogue(new Dialogue.DialogueLine[]
            {
                new Dialogue.DialogueLine { characterName = "Narrator", line = "Perfect score!" }
            });
        }
        else
        {
            dialogue.StartDialogue(new Dialogue.DialogueLine[]
            {
                new Dialogue.DialogueLine { characterName = "Narrator", line = $"You scored {score} / {quizQuestions.Length}." }
            });
        }
    }
}

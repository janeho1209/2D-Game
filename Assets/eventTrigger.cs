using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [Header("Interaction Settings")]
    public string targetTag; // e.g. "Hole", "Door", "Table"

    [Header("Dialogue to Trigger")]
    public Dialogue.DialogueLine[] triggerDialogue;

    [Header("Other Options")]
    public bool destroyThis = false;
    public bool destroyTarget = false;

    private Dialogue dialogueManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<Dialogue>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            // Trigger dialogue if assigned
            if (triggerDialogue != null && triggerDialogue.Length > 0 && dialogueManager != null)
            {
                dialogueManager.StartDialogue(triggerDialogue);
            }

            // Destroy either or both
            if (destroyThis)
                Destroy(gameObject);
            if (destroyTarget)
                Destroy(other.gameObject);
        }
    }
}

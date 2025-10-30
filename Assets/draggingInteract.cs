using UnityEngine;

public class DraggingInteract : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool dragging = false;
    private GameObject objectBeingDragged;
    private Dialogue dialogue;
    public Dialogue.DialogueLine[] triggerDialogue;
    public string targetTag = "Hole";

    void Start()
    {
        originalPosition = transform.position;
        dialogue = FindObjectOfType<Dialogue>();
    }

    void OnMouseDown()
    {
        dragging = true;
        objectBeingDragged = gameObject;
    }

    void OnMouseUp()
    {
        dragging = false;

        // Check if we’re over the target
        Collider2D target = Physics2D.OverlapPoint(transform.position);
        if (target != null && target.CompareTag(targetTag))
        {
            // Interaction successful: destroy both
            Destroy(target.gameObject);
            Destroy(gameObject);
            dialogue.StartDialogue(triggerDialogue);
        }
        else
        {
            // Not over valid target return to original spot
            transform.position = originalPosition;
        }

        objectBeingDragged = null;
    }

    void Update()
    {
        if (dragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0; // keep on 2D plane
            transform.position = mousePos;
        }
    }
}

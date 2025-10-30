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

        Vector2 pos = transform.position;
        Collider2D[] hitsCircle = Physics2D.OverlapCircleAll(pos, 0);
        Collider2D found = null;
        foreach (var c in hitsCircle)
        {
            if (c.CompareTag(targetTag))
            {
                found = c;
                break;
            }
        }

        if (found != null) //trigger an interaction
        {
            Destroy(found.gameObject);
            Destroy(gameObject);
            dialogue.StartDialogue(triggerDialogue);
        }
        else //if not interactable, return to original position
        {
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

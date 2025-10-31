using UnityEngine;

public class DraggingInteract : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool dragging = false;
    private GameObject objectBeingDragged;
    private Dialogue dialogue;
    public Dialogue.DialogueLine[] triggerDialogue;
    public string targetTag = "Hole";
    private AudioSource audioSource;


    void Start()
    {
        originalPosition = transform.position;
        dialogue = FindObjectOfType<Dialogue>();
        GameObject audioGO = GameObject.Find("willypencil");
        if (audioGO != null)
        {
            audioSource = audioGO.GetComponent<AudioSource>();
        }
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
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }
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

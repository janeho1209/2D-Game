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

    void OnMouseDown() //drag the object
    {
        dragging = true;
        objectBeingDragged = gameObject;
    }

    void OnMouseUp()
    {
        dragging = false;

        Vector2 pos = transform.position;
        Collider2D[] hitsCircle = Physics2D.OverlapCircleAll(pos, 0); //get a collection of the points around the cursor
        Collider2D found = null;
        foreach (var i in hitsCircle) //see if any of the points are on an interactable asset
        {
            if (i.CompareTag(targetTag))
            {
                found = i;
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
        if (dragging) //making sure the asset follows the cursor
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
    }
}

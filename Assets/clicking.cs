using UnityEngine;

public class clicking : MonoBehaviour
{
    public Sprite newSprite;  //if clicking changes to a new sprite
    public bool destroyOnClick = false; //if clicking maks the sprite disappear
    SpriteRenderer spriteRenderer;
    private Dialogue dialogue;
    public Dialogue.DialogueLine[] triggerDialogue;
    private bool flag = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        dialogue = FindObjectOfType<Dialogue>();
    }

    void OnMouseDown()
    {
        if (newSprite != null) //if there's a new sprite attached to gameobject
        {
            spriteRenderer.sprite = newSprite;
        }

        if (destroyOnClick) //if the variable is checked off
        {
            dialogue.StartDialogue(triggerDialogue);
            Destroy(gameObject);
        }
        //trigger some noises here
    }

    private void OnMouseUp()
    {
        if (flag == false)
        { 
            dialogue.StartDialogue(triggerDialogue);
            flag = true;
        }
    }
}

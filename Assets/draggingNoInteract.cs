using UnityEngine;

public class draggingNoInteract : MonoBehaviour
{
    Vector3 basePosition;
    bool dragging = false;
    public Sprite baseSprite;    // assign in Inspector
    public Sprite dragSprite;    // assign the "dragging" version
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        basePosition = transform.position;
    }

    void OnMouseDown()
    {
        dragging = true;
        if (dragSprite != null)
            spriteRenderer.sprite = dragSprite;  // change to dragging version
    }

    void OnMouseUp()
    {
        dragging = false;
        transform.position = basePosition;       // return to original spot
        spriteRenderer.sprite = baseSprite;      // revert to original sprite
    }

    void Update()
    {
        if (dragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;  // keep sprite on 2D plane
            transform.position = mousePos;
        }
    }
}

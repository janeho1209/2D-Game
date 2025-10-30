using UnityEngine;

public class ClickTester : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 pos2D = new Vector2(worldPos.x, worldPos.y);

            RaycastHit2D hit = Physics2D.Raycast(pos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log("Clicked on: " + hit.collider.name);
            }
            else
            {
                Debug.Log("Clicked on nothing");
            }
        }
    }
}

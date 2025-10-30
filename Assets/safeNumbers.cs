using UnityEngine;

public class SafeNumbers : MonoBehaviour
{
    public int number; //number from 1-6
    public SafeController safeController;

    void OnMouseDown()
    {
        safeController.UserInput(number);
    }
}

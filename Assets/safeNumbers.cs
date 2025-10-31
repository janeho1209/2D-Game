using UnityEngine;

public class SafeNumbers : MonoBehaviour
{
    public int number; //number from 1-6
    public SafeController safeController;
    private AudioSource audioSource;

    void OnMouseDown()
    {
        GameObject audioGO = GameObject.Find("beep");
        if (audioGO != null)
        {
            audioSource = audioGO.GetComponent<AudioSource>();
        }
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
        safeController.UserInput(number);
    }
}

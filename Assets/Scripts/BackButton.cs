using UnityEngine;

public class BackButton : MonoBehaviour
{
    // Poziva se kada se dugme klikne
    public void OnBackButtonClick()
    {
        Debug.Log("Izlaz iz aplikacije");

        // Ako je build aplikacija
        Application.Quit();

        // Ako si u editoru, samo za test
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

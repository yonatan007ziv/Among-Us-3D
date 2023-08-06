using UnityEngine;

public class Startup : MonoBehaviour
{
    // Main Menu Startup
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        QualitySettings.vSyncCount = 0;
    }
}
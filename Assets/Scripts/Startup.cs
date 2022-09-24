using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{
    //main menu startup
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        QualitySettings.vSyncCount = 0;
    }
}

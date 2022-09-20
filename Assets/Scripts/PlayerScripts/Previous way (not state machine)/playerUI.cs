using UnityEngine;

public class playerUI : MonoBehaviour
{
    public bool uiOpen;
    public bool menuOpen;
    public bool mapOpen;

    public GameObject menu;
    public GameObject map;
    public GameObject currentUIObject;
    public GameObject playerCanvas;

    private void Start()
    {
        playerCanvas.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuOpen)
                closeMenu();
            else
            {
                if (uiOpen)
                    closeUI();
                else if (mapOpen)
                    closeMap();
                else
                    openMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.M) && map != null)
        {
            if (mapOpen)
                closeMap();
            else
            {
                if (uiOpen)
                    closeUI();
                else if (menuOpen)
                    closeMenu();
                else
                    openMap();
            }
        }
    }
    public void closeUI()
    {
        if (currentUIObject == null)
            return;
        currentUIObject.GetComponent<use>().useFunc();
    }
    public void openMenu()
    {
        if (menu == null)
            return;
        Cursor.lockState = CursorLockMode.Confined;
        menu.SetActive(true);
        menuOpen = true;
    }
    public void closeMenu()
    {
        if (menu == null)
            return;
        Cursor.lockState = CursorLockMode.Locked;
        menu.SetActive(false);
        menuOpen = false;
    }
    public void openMap()
    {
        if (map == null)
            return;
        Cursor.lockState = CursorLockMode.Confined;
        map.SetActive(true);
        mapOpen = true;
    }
    public void closeMap()
    {
        if (map == null)
            return;
        Cursor.lockState = CursorLockMode.Locked;
        map.SetActive(false);
        mapOpen = false;
    }
    public void closeAllUI()
    {
        closeMap();
        closeMenu();
        closeUI();
    }
}
using UnityEngine;

public class PlayerUI : MonoBehaviour
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
                CloseMenu();
            else
            {
                if (uiOpen)
                    CloseUI();
                else if (mapOpen)
                    CloseMap();
                else
                    OpenMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.M) && map != null)
        {
            if (mapOpen)
                CloseMap();
            else
            {
                if (uiOpen)
                    CloseUI();
                else if (menuOpen)
                    CloseMenu();
                else
                    OpenMap();
            }
        }
    }
    public void CloseUI()
    {
        if (currentUIObject == null)
            return;
        currentUIObject.GetComponent<Use>().UseFunc();
    }
    public void OpenMenu()
    {
        if (menu == null)
            return;
        Cursor.lockState = CursorLockMode.Confined;
        menu.SetActive(true);
        menuOpen = true;
    }
    public void CloseMenu()
    {
        if (menu == null)
            return;
        Cursor.lockState = CursorLockMode.Locked;
        menu.SetActive(false);
        menuOpen = false;
    }
    public void OpenMap()
    {
        if (map == null)
            return;
        Cursor.lockState = CursorLockMode.Confined;
        map.SetActive(true);
        mapOpen = true;
    }
    public void CloseMap()
    {
        if (map == null)
            return;
        Cursor.lockState = CursorLockMode.Locked;
        map.SetActive(false);
        mapOpen = false;
    }
    public void CloseAllUI()
    {
        CloseMap();
        CloseMenu();
        CloseUI();
    }
}
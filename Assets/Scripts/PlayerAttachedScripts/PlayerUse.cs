using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUse : MonoBehaviour
{
    public Transform currentUseable;
    public List<Transform> useables = new List<Transform>();
    private float distanceForUse = 5;
    public float currentDistanceFromUse = 5;
    public Button useButton;
    private PlayerUI playerUI;
    private void Start()
    {
        playerUI = GetComponent<PlayerUI>();
        RefreshUseables();
    }
    public void RefreshUseables()
    {
        useables.Clear();
        foreach (GameObject currentUseableForeach in GameObject.FindGameObjectsWithTag("Useable"))
            useables.Add(currentUseableForeach.transform);
        scanUseables();
    }

    private void Update()
    {
        scanUseables();
        if (Input.GetKeyDown(KeyCode.F) && currentUseable != null)
            use();
    }
    void scanUseables()
    {
        foreach (Transform useable in useables)
        {
            if (useable == null)
                continue;
            float distance = Vector3.Distance(transform.position, new Vector3(useable.transform.position.x, transform.position.y, useable.position.z));

            if (distance < currentDistanceFromUse)
            {
                useButton.interactable = true;
                currentUseable = useable;
                currentDistanceFromUse = distance;
            }
            else if (distance > distanceForUse && currentUseable == useable || currentUseable == null)
            {
                useButton.interactable = false;
                currentUseable = null;
                currentDistanceFromUse = distanceForUse;
            }
        }
    }
    public void use()
    {
        if (currentUseable == null)
            return;

        if (playerUI.menuOpen)
            playerUI.CloseMenu();
        if (playerUI.mapOpen)
            playerUI.CloseMap();

        currentUseable.GetComponent<Use>().UseFunc();
    }
}
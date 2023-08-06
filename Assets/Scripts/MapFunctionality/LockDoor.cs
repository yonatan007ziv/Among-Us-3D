using Mirror;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockDoor : MonoBehaviour
{
    public int timer = 40;
    private bool doorsLocked;

    public void Lock()
    {
        if (!doorsLocked)
        {

            doorsLocked = true;
            foreach (Transform c in transform)
            {
                foreach (Transform child in c)
                {
                    if (child.name.Contains("DoorLocked"))
                        child.GetComponent<MeshRenderer>().enabled = true;
                    else if (child.name.Contains("DoorUnlocked"))
                        child.GetComponent<MeshRenderer>().enabled = false;
                    else if (child.name.Contains("Collider"))
                        child.GetComponent<BoxCollider>().enabled = true;
                }
            }

            GameObject localPlayer = NetworkClient.localPlayer.gameObject;
            if (localPlayer.GetComponent<PlayerStateMachine>() is ImposterStateMachine)
                StartCoroutine(LockTimer(localPlayer));
            else
                StartCoroutine(LockNoTimer());
        }
    }

    IEnumerator LockNoTimer()
    {
        while (timer > 30)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }
        foreach (Transform c in transform)
        {
            foreach (Transform child in c)
            {
                if (child.name.Contains("DoorLocked"))
                    child.GetComponent<MeshRenderer>().enabled = false;
                else if (child.name.Contains("DoorUnlocked"))
                    child.GetComponent<MeshRenderer>().enabled = true;
                else if (child.name.Contains("Collider"))
                    child.GetComponent<BoxCollider>().enabled = false;
            }
        }
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }
        doorsLocked = false;
        timer = 40;
    }

    IEnumerator LockTimer(GameObject imposter)
    {
        Transform btn = imposter.transform.Find("PlayerCanvas/ImposterMap/" + gameObject.name);
        btn.GetComponent<Button>().interactable = false;
        TextMeshProUGUI mapTimer = btn.GetComponentInChildren<TextMeshProUGUI>();
        while (timer > 30)
        {
            mapTimer.text = timer.ToString();
            yield return new WaitForSeconds(1);
            timer--;
            mapTimer.text = timer.ToString();
        }
        foreach (Transform c in transform)
        {
            foreach (Transform child in c)
            {
                if (child.name.Contains("DoorLocked"))
                    child.GetComponent<MeshRenderer>().enabled = false;
                else if (child.name.Contains("DoorUnlocked"))
                    child.GetComponent<MeshRenderer>().enabled = true;
                else if (child.name.Contains("Collider"))
                    child.GetComponent<BoxCollider>().enabled = false;
            }
        }
        while (timer > 0)
        {
            mapTimer.text = timer.ToString();
            yield return new WaitForSeconds(1);
            timer--;
            mapTimer.text = timer.ToString();
        }
        btn.GetComponent<Button>().interactable = true;
        mapTimer.text = "";
        doorsLocked = false;
        timer = 40;
    }
}
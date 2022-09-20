using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
public class LockDoor : MonoBehaviour
{
    public int timer = 40;
    private bool doorsLocked;

    public void lockDoor()
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
            if (localPlayer.GetComponent<playerState>().isImposter)
                StartCoroutine(lockDoorTimer(localPlayer));
            else
                StartCoroutine(lockDoorTimer());
        }
    }

    IEnumerator lockDoorTimer()
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
    IEnumerator lockDoorTimer(GameObject imposter) // with timer
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
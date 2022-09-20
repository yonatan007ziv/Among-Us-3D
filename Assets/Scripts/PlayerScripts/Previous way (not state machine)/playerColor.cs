using UnityEngine;
using Mirror;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerColor : NetworkBehaviour
{
    public string currentColor;
    public string previousColor = "";
    public SkinnedMeshRenderer skinnedMeshRenderer;
    private string[] allColors = new string[] { "BabyBlue", "Black", "Blue", "Brown", "Crystal", "Cyan", "Green", "Lime", "Orange", "Pink", "Purple", "Red", "White", "Yellow" };
    public void changeColor(string color)
    {
        if (isServer)
            changeColorRpc(color);
        else
            changeColorCmd(color);
    }
    [Command]
    private void changeColorCmd(string color)
    {
        changeColorRpc(color);
    }
    [ClientRpc]
    private void changeColorRpc(string color)
    {
        Material[] mats = new Material[3];
        mats[0] = Resources.Load<Material>("Materials/Black/Astronaut_Outline_Black");
        mats[1] = Resources.Load<Material>("Materials/" + color + "/Astronaut_" + color);
        mats[2] = Resources.Load<Material>("Materials/" + color + "/Astronaut_Backpack_" + color);
        skinnedMeshRenderer.materials = mats;
        currentColor = color;

        if (SceneManager.GetActiveScene().name == "Lobby" && color != previousColor)
        {
            GameObject.FindGameObjectWithTag("colorTab").GetComponent<colorTabHolder>().colorTab.Find(color).GetComponent<Button>().interactable = false;
            if (previousColor != "")
                GameObject.FindGameObjectWithTag("colorTab").GetComponent<colorTabHolder>().colorTab.Find(previousColor).GetComponent<Button>().interactable = true;
        }
        previousColor = color;
    }
    public IEnumerator randomColor()
    {
        string randColor = allColors[Random.Range(0, allColors.Length)];
        foreach (playerColor c in FindObjectsOfType<playerColor>())
        {
            if (c == this)
                continue;

            while (c.currentColor == "")
            {
                Debug.Log("Waiting for " + c.name + "'s color...");
                yield return new WaitForEndOfFrame();
            }
            if (c.currentColor == randColor)
            {
                Debug.LogError(randColor + " is taken! switching...");
                StartCoroutine(randomColor());
                yield break;
            }
        }
        changeColor(randColor);
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColor : MonoBehaviour
{
    public SkinnedMeshRenderer myMeshRenderer;
    public string currentColor;
    private string previousColor = "";
    private readonly ReadOnlyCollection<string> AllColors = new ReadOnlyCollection<string>(new List<string> { "BabyBlue", "Black", "Blue", "Brown", "Crystal", "Cyan", "Green", "Lime", "Orange", "Pink", "Purple", "Red", "White", "Yellow" });

    public void SetColor(string color)
    {
        currentColor = color;
        Material[] mats = new Material[3];
        mats[0] = Resources.Load<Material>("Materials/Black/Astronaut_Outline_Black");
        mats[1] = Resources.Load<Material>("Materials/" + color + "/Astronaut_" + color);
        mats[2] = Resources.Load<Material>("Materials/" + color + "/Astronaut_Backpack_" + color);
        myMeshRenderer.materials = mats;

        if (SceneManager.Instance.CurrentSceneName == "Lobby" && color != previousColor)
        {
            DeOccupyColor(previousColor, false);
            DeOccupyColor(color, true);
        }

        previousColor = color;
    }

    private void DeOccupyColor(string color, bool occupy)
    {
        if (color != "")
            GameObject.FindGameObjectWithTag("ColorTab").GetComponent<ColorTabHolder>().colorTab.Find(color).GetComponent<Button>().interactable = !occupy;
    }

    public void SetColorMessage(string color)
    {
        //PlayerMessages.SetLocalColor(color);
    }

    public IEnumerator SetRandomColor()
    {
        string randColor = AllColors[Random.Range(0, AllColors.Count)];
        foreach (PlayerColor c in FindObjectsOfType<PlayerColor>())
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
                StartCoroutine(SetRandomColor());
                yield break;
            }
        }
        SetColorMessage(randColor);
    }
}
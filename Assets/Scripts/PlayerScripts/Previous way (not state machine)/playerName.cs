using Mirror;
using TMPro;
using UnityEngine;

public class playerName : MonoBehaviour
{
    public TextMeshPro nameTag;
    public string currentName = "";
    private string oldName = "";

    private void Start()
    {
        InvokeRepeating(nameof(checkName), 0, 1);
    }

    private void checkName()
    {
        if (currentName != oldName)
        {
            nameTag.text = currentName;
            oldName = currentName;
        }
    }
}
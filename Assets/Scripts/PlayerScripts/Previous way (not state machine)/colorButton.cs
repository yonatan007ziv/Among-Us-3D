using UnityEngine;
using Mirror;

public class colorButton : MonoBehaviour
{
    public void changeColor(string color)
    {
        NetworkClient.localPlayer.GetComponent<playerColor>().changeColor(color);
    }
}
using UnityEngine;
using Mirror;

public class ColorButton : MonoBehaviour
{
    public void changeColor(string color)
    {
        NetworkClient.localPlayer.GetComponent<PlayerColor>().SetColor(color);
    }
}
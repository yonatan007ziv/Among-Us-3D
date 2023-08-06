using Mirror;
using UnityEngine;

public class UseLobbyComputer : Use
{
    public GameObject lobbyCanvas;
    public override void UseFunc()
    {
        base.UseFunc();
        lobbyCanvas.SetActive(!lobbyCanvas.activeSelf);
        NetworkClient.localPlayer.GetComponent<PlayerUI>().currentUIObject = lobbyCanvas.activeSelf ? gameObject : null;
    }
}

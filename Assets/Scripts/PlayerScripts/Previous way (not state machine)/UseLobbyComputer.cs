using Mirror;
using UnityEngine;

public class UseLobbyComputer : use
{
    public GameObject lobbyCanvas;
    public override void useFunc()
    {
        base.useFunc();
        lobbyCanvas.SetActive(!lobbyCanvas.activeSelf);
        NetworkClient.localPlayer.GetComponent<playerUI>().currentUIObject = lobbyCanvas.activeSelf ? gameObject : null;
    }
}

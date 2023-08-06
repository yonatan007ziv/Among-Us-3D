using Mirror;
using UnityEngine;

public class PlayerConnect : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        if (!isLocalPlayer)
            return;

        GameObject localPlayer = netIdentity.gameObject;

        localPlayer.transform.Find("LogicPoints/Eyes/PlayerCamera").gameObject.SetActive(true);
        localPlayer.transform.Find("Visual/PlayerCanvas").gameObject.SetActive(true);

        localPlayer.GetComponent<PlayerRotationHeadSpine>().enabled = true;
        localPlayer.GetComponent<PlayerStateMachine>().enabled = true;
        localPlayer.GetComponent<PlayerLean>().enabled = true;
        localPlayer.GetComponent<PlayerLook>().enabled = true;
        localPlayer.GetComponent<PlayerUse>().enabled = true;
        localPlayer.GetComponent<PlayerUI>().enabled = true;

        localPlayer.transform.Find("Visual/AstronautMesh").gameObject.layer = LayerMask.NameToLayer("LocalPlayer");
        localPlayer.transform.Find("Visual/NameTag").gameObject.layer = LayerMask.NameToLayer("LocalPlayer");

        PlayerMessages messagesBetweenPlayers = localPlayer.GetComponent<PlayerMessages>();
        messagesBetweenPlayers.GetNames();
        messagesBetweenPlayers.GetColors();
        messagesBetweenPlayers.DoNotDestroyPlayerOnLoad();
        StartCoroutine(localPlayer.GetComponent<PlayerColor>().SetRandomColor());
    }
}
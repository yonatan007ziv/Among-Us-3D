using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class playerConnect : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        GameObject localPlayer = netIdentity.gameObject;

        localPlayer.transform.Find("Eyes/PlayerCamera").gameObject.SetActive(true);
        localPlayer.transform.Find("PlayerCanvas").gameObject.SetActive(true);

        localPlayer.GetComponent<playerRotationHeadSpine>().enabled = true;
        localPlayer.GetComponent<playerState>().enabled = true;
        localPlayer.GetComponent<playerLean>().enabled = true;
        localPlayer.GetComponent<playerLook>().enabled = true;
        localPlayer.GetComponent<playerUse>().enabled = true;
        localPlayer.GetComponent<playerUI>().enabled = true;

        localPlayer.transform.Find("AstronautMesh").gameObject.layer = LayerMask.NameToLayer("LocalPlayer");
        localPlayer.transform.Find("NameTag").gameObject.layer = LayerMask.NameToLayer("LocalPlayer");

        messagesBetweenPlayers messagesBetweenPlayers = localPlayer.GetComponent<messagesBetweenPlayers>();
        messagesBetweenPlayers.getNames();
        messagesBetweenPlayers.getColors();
        messagesBetweenPlayers.getDoNotDestroyOnLoad();
        StartCoroutine(localPlayer.GetComponent<playerColor>().randomColor());
    }
}

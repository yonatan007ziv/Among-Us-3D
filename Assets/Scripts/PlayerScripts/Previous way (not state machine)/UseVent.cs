using UnityEngine;
using Mirror;
public class UseVent : use
{
    public override void useFunc()
    {
        GameObject player = NetworkClient.localPlayer.gameObject;
        playerVent playerVent = player.GetComponent<playerVent>();
        playerState playerState = player.GetComponent<playerState>();

        if (!playerVent.finishedInterpolating || playerVent.movingVents)
            return;
        
        playerState.inputDisabled = true;
        StartCoroutine(NetworkClient.localPlayer.GetComponent<playerVent>().useVent(transform));
    }
}
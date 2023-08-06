using UnityEngine;
using Mirror;
public class UseVent : Use
{
    public override void UseFunc()
    {
        GameObject player = NetworkClient.localPlayer.gameObject;
        PlayerVent playerVent = player.GetComponent<PlayerVent>();
        PlayerStateMachine playerSM = player.GetComponent<PlayerStateMachine>();

        //if (!playerVent.finishedInterpolating || playerVent.movingVents)
        //    return;
        
        playerSM.ChangeState(playerSM.InputDisabledState);
        StartCoroutine(NetworkClient.localPlayer.GetComponent<PlayerVent>().UseVent(transform));
    }
}
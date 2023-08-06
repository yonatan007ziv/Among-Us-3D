using Mirror;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Use : MonoBehaviour
{
    public virtual void UseFunc()
    {
        PlayerStateMachine playerState = NetworkClient.localPlayer.GetComponent<PlayerStateMachine>();

        if (playerState.CurrentState is InputDisabledBase)
            playerState.ChangeState(playerState.IdleState);
        else
            playerState.ChangeState(playerState.InputDisabledState);

        PlayerUI playerUI = NetworkClient.localPlayer.GetComponent<PlayerUI>();
        playerUI.uiOpen = !playerUI.uiOpen;

        Cursor.lockState = Cursor.lockState == CursorLockMode.Confined ? CursorLockMode.Locked : CursorLockMode.Confined;
    }
}
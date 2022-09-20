using Mirror;
using UnityEngine;

public class use : MonoBehaviour
{
    public virtual void useFunc()
    {
        playerState playerState = NetworkClient.localPlayer.GetComponent<playerState>();
        playerState.inputDisabled = !playerState.inputDisabled;

        playerUI playerUI = NetworkClient.localPlayer.GetComponent<playerUI>();
        playerUI.uiOpen = !playerUI.uiOpen;

        Cursor.lockState = Cursor.lockState == CursorLockMode.Confined ? CursorLockMode.Locked : CursorLockMode.Confined;
    }
}
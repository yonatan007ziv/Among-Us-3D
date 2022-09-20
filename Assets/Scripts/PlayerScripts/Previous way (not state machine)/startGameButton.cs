using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class startGameButton : MonoBehaviour
{
    public Button btn;
    private NetworkManager currentManager = NetworkManager.singleton;

    void Update()
    {
        if (currentManager.numPlayers < 1)
            btn.interactable = false;
        else
            btn.interactable = true;
        if (Input.GetKeyDown(KeyCode.R) && btn.interactable)
            NetworkClient.localPlayer.GetComponent<messagesBetweenPlayers>().startGameRpc();
    }
}
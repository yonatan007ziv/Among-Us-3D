using Mirror;
using UnityEngine;

public class HostClientOpenerLobby : MonoBehaviour
{
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("hostTag") != null)
        {
            NetworkManager.singleton.StartHost();
            Instantiate(Resources.Load<GameObject>("LobbyShit/LobbyStartButton"));
        }
        else if (GameObject.FindGameObjectWithTag("clientTag") != null)
        {
            NetworkManager.singleton.StartClient();
            Instantiate(Resources.Load<GameObject>("LobbyShit/LobbyStartText"));
        }
    }
}
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(StartGame);
    }

    void Update()
    {
        if (NetworkManager.singleton.numPlayers < 1)
            btn.interactable = false;
        else
            btn.interactable = true;
        if (Input.GetKeyDown(KeyCode.R) && btn.interactable)
            StartGame();
    }

    private void StartGame()
    {
        NetworkClient.localPlayer.GetComponent<PlayerMessages>().StartGameRpc();
    }
}
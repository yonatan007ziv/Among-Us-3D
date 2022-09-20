using System;
using System.Collections;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Threading;
public class CustomNetworkSystem : NetworkManager
{
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        StartCoroutine(initializePlayer(conn));
    }
    public IEnumerator initializePlayer(NetworkConnection conn)
    {
        while (conn.identity == null || conn.identity.gameObject == null)
            yield return new WaitForEndOfFrame();
    }
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        GameObject server = NetworkClient.localPlayer.gameObject;
        server.GetComponent<messagesBetweenPlayers>().joinLeaveMessage(conn.identity.name + " has Left the game!", Color.red);
        server.GetComponent<messagesBetweenPlayers>().stopStartGame();
        base.OnServerDisconnect(conn);
    }
    public override void OnClientChangeScene(string newSceneName, SceneOperation sceneOperation, bool customHandling)
    {
        base.OnClientChangeScene(newSceneName, sceneOperation, customHandling);
        NetworkClient.localPlayer.GetComponent<playerUI>().closeAllUI();
        NetworkClient.localPlayer.GetComponent<CharacterController>().enabled = false;
        //enabling later on in messagesBetweenPlayers
    }
    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn);
        conn.identity.GetComponent<messagesBetweenPlayers>().readyUp();
        conn.identity.GetComponent<playerUse>().refreshUseables();
    }
    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        StartCoroutine(joinMessage(conn));
    }
    IEnumerator joinMessage(NetworkConnection conn)
    {
        while (conn.identity == null || conn.identity.transform.Find("NameTag").GetComponent<TextMeshPro>().text == "")
            yield return new WaitForEndOfFrame();

        messagesBetweenPlayers serverMsg = NetworkClient.localPlayer.gameObject.GetComponent<messagesBetweenPlayers>();
        serverMsg.joinLeaveMessage(conn.identity.name + " has Joined the game!", Color.green);
        serverMsg.stopStartGame();
    }
    public override void OnClientError(Exception exception)
    {
        base.OnClientError(exception);
        if (NetworkClient.connection.identity.isServer)
            NetworkManager.singleton.StopHost();
        SceneManager.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.Confined;
    }
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        if (conn.identity.isServer)
            NetworkManager.singleton.StopHost();
        SceneManager.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.Confined;
    }
}
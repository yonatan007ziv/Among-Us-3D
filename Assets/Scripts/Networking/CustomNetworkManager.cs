using Mirror;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        if (conn.identity.isServer)
            Instantiate(Resources.Load<GameObject>("Lobby/StartGameButton"), conn.identity.transform.Find("Visual/PlayerCanvas"));
        else
            Instantiate(Resources.Load<GameObject>("Lobby/StartGameText"), conn.identity.transform.Find("Visual/PlayerCanvas"));
    }

    public Task WaitForPlayerObject(NetworkConnection conn)
    {
        while (conn.identity == null || conn.identity.gameObject == null) { Task.Delay(1); }
        return Task.CompletedTask;
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);

        //await WaitForPlayerObject(conn);
        //NetworkClient.localPlayer.gameObject.GetComponent<PlayerMessages>().InterruptStartGame();
        JoinMessage(conn);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        NetworkClient.localPlayer.gameObject.GetComponent<PlayerMessages>().InterruptStartGame();
        LeaveMessage(conn);
        base.OnServerDisconnect(conn);
    }

    private async void JoinMessage(NetworkConnection joinedConn)
    {
        await WaitForPlayerObject(joinedConn);

        PlayerMessages hostPlayerMessages = NetworkClient.localPlayer.gameObject.GetComponent<PlayerMessages>();
        hostPlayerMessages.JoinLeaveMessage($"{joinedConn.identity.name} has Joined the game!", Color.green);
        hostPlayerMessages.InterruptStartGame();
    }

    private async void LeaveMessage(NetworkConnection leftConn)
    {
        await WaitForPlayerObject(leftConn);

        PlayerMessages hostPlayerMessages = NetworkClient.localPlayer.gameObject.GetComponent<PlayerMessages>();
        hostPlayerMessages.JoinLeaveMessage($"{leftConn.identity.name} has Left the game!", Color.red);
        hostPlayerMessages.InterruptStartGame();
    }

    public override void OnClientChangeScene(string newSceneName, SceneOperation sceneOperation, bool customHandling)
    {
        base.OnClientChangeScene(newSceneName, sceneOperation, customHandling);
        NetworkClient.localPlayer.GetComponent<PlayerUI>().CloseAllUI();
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn);
        conn.identity.GetComponent<PlayerMessages>().Ready();
        conn.identity.GetComponent<PlayerUse>().RefreshUseables();
    }

    public override void OnClientError(Exception exception)
    {
        base.OnClientError(exception);
        if (NetworkClient.connection.identity.isServer)
            StopHost();
        SceneManager.Instance.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.Confined;
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        if (conn.identity.isServer)
            StopHost();
        SceneManager.Instance.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.Confined;
    }
}
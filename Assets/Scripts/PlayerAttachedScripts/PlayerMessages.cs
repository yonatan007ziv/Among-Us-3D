using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class PlayerMessages : NetworkBehaviour
{
    #region ready logic
    public int readyCounter = 0;

    public void Ready()
    {
        if (isServer)
        {
            NetworkClient.localPlayer.GetComponent<PlayerMessages>().readyCounter++;
            if (NetworkManager.singleton.numPlayers == readyCounter)
            {
                RandomSpawnPoints();
                AssignRoles();
                RollIntro();
            }
        }
        else
            ReadyCmd();
    }

    [Command]
    void ReadyCmd()
    {
        NetworkClient.localPlayer.GetComponent<PlayerMessages>().readyCounter++;
        if (NetworkManager.singleton.numPlayers == NetworkClient.localPlayer.GetComponent<PlayerMessages>().readyCounter)
        {
            RandomSpawnPoints();
            AssignRoles();
            RollIntro();
        }
    }
    #endregion

    #region dont destroy on load
    public void DoNotDestroyPlayerOnLoad()
    {
        if (isServer)
            DoNotDestroyPlayerOnLoadRpc(NetworkClient.localPlayer.gameObject);
        else
            DoNotDestroyPlayerOnLoadCmd(NetworkClient.localPlayer.gameObject);
    }

    [Command]
    private void DoNotDestroyPlayerOnLoadCmd(GameObject identity)
    {
        DoNotDestroyPlayerOnLoadRpc(identity);
    }
    [ClientRpc]
    private void DoNotDestroyPlayerOnLoadRpc(GameObject identity)
    {
        DontDestroyOnLoad(identity);
    }
    #endregion

    #region change player visibility
    #region vent skinned mesh renderer
    public void skinnedMeshRendererEnable(bool enable)
    {
        if (isServer)
            skinnedMeshRendererEnableRpc(enable);
        else
            skinnedMeshRendererEnableCmd(enable);
    }
    [ClientRpc]
    void skinnedMeshRendererEnableRpc(bool enable)
    {
        transform.Find("AstronautMesh").GetComponent<SkinnedMeshRenderer>().enabled = enable;
    }
    [Command]
    void skinnedMeshRendererEnableCmd(bool enable)
    {
        skinnedMeshRendererEnableRpc(enable);
    }
    #endregion
    #region vent text mesh pro
    public void TextMeshProEnable(bool enable)
    {
        if (isServer)
            TextMeshProEnableRpc(enable);
        else
            TextMeshProEnableCmd(enable);
    }
    [ClientRpc]
    void TextMeshProEnableRpc(bool enable)
    {
        transform.Find("NameTag").GetComponent<TextMeshPro>().enabled = enable;
    }
    [Command]
    void TextMeshProEnableCmd(bool enable)
    {
        TextMeshProEnableRpc(enable);
    }
    #endregion
    #endregion

    #region doors

    public void LockDoor(string door)
    {
        if (isServer)
            LockDoorRpc(door);
        else
            LockDoorsCmd(door);
    }

    [Command]
    private void LockDoorsCmd(string door)
    {
        LockDoorRpc(door);
    }

    [ClientRpc]
    private void LockDoorRpc(string door)
    {
        GameObject.FindWithTag(door).GetComponent<LockDoor>().Lock();
    }

    #endregion

    #region Name Tag System

    public void GetNames()
    {
        if (isServer)
            GetNamesRpc();
        else
            GetNamesCmd();
    }

    [Command]
    private void GetNamesCmd()
    {
        GetNamesRpc();
    }

    [ClientRpc]
    private void GetNamesRpc()
    {
        SetName(NetworkClient.localPlayer.gameObject, UserDataManager.Instance.Username);
    }

    private void SetName(GameObject localPlayer, string playerName)
    {
        if (isServer)
            SetNamesRpc(localPlayer, playerName);
        else
            SetNamesCmd(localPlayer, playerName);
    }

    [Command]
    private void SetNamesCmd(GameObject formerLocalPlayerObj, string playerName)
    {
        SetNamesRpc(formerLocalPlayerObj, playerName);
    }

    [ClientRpc]
    private void SetNamesRpc(GameObject formerLocalPlayerObj, string playerName)
    {
        bool onlySpaces = true;

        foreach (char c in playerName)
            if (c != ' ')
                onlySpaces = false;

        if (onlySpaces)
            playerName = "Nice try";

        formerLocalPlayerObj.transform.Find("Visual/NameTag").GetComponent<TextMeshPro>().text = playerName;
        formerLocalPlayerObj.name = playerName;
    }

    #endregion

    #region Color System

    public void GetColors()
    {
        if (isServer)
            GetColorsRpc();
        else
            GetColorsCmd();
    }

    [Command]
    private void GetColorsCmd()
    {
        GetColorsRpc();
    }

    [ClientRpc]
    private void GetColorsRpc()
    {
        GameObject localPlayer = NetworkClient.localPlayer.gameObject;
        SetColor(localPlayer, localPlayer.GetComponent<PlayerColor>().currentColor);
    }

    public void SetLocalColor(string color)
    {
        SetColor(NetworkClient.localPlayer.gameObject, color);
    }

    public void SetColor(GameObject formerLocalPlayerObj, string color)
    {
        if (isServer)
            SetColorRpc(formerLocalPlayerObj, color);
        else
            SetColorCmd(formerLocalPlayerObj, color);
    }

    [Command]
    private void SetColorCmd(GameObject formerLocalPlayerObj, string color)
    {
        SetColorRpc(formerLocalPlayerObj, color);
    }

    [ClientRpc]
    private void SetColorRpc(GameObject formerLocalPlayerObj, string color)
    {
        if (color != "")
            formerLocalPlayerObj.GetComponent<PlayerColor>().SetColor(color);
    }

    #endregion

    #region join leave messages

    public void JoinLeaveMessage(string msg, Color color)
    {
        if (isServer)
            joinLeaveMessageRpc(msg, color);
        else
            joinLeaveMessageCmd(msg, color);
    }

    [Command]
    private void joinLeaveMessageCmd(string msg, Color color)
    {
        joinLeaveMessageRpc(msg, color);
    }

    [ClientRpc]
    private void joinLeaveMessageRpc(string msg, Color color)
    {
        GameObject joinLeaveMessage = Instantiate(Resources.Load<GameObject>("ChatTemplates/JoinLeaveChatTemplate"), NetworkClient.localPlayer.transform.Find("PlayerCanvas/LobbyMessageSorter"));
        TextMeshProUGUI messageComponent = joinLeaveMessage.GetComponent<TextMeshProUGUI>();
        messageComponent.color = color;
        messageComponent.text = msg;
        Destroy(joinLeaveMessage, 5);
    }

    #endregion

    #region start game logic (roles spawn points and intro)
    #region get to random spawn points
    void RandomSpawnPoints()
    {
        List<NetworkIdentity> players = GameObject.FindGameObjectsWithTag("Player").Select(p => p.GetComponent<NetworkIdentity>()).ToList();
        List<Vector3> spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints").Select(p => p.transform.position).ToList();

        foreach (NetworkIdentity i in players)
        {
            int rand = Random.Range(0, spawnPoints.Count);
            MoveTo(i, spawnPoints[rand]);
            spawnPoints.RemoveAt(rand);
        }
    }
    void MoveTo(NetworkIdentity identity, Vector3 pos)
    {
        if (isServer)
            MoveToRpc(identity, pos);
        else
            MoveToCmd(identity, pos);
    }
    [ClientRpc]
    void MoveToRpc(NetworkIdentity identity, Vector3 pos)
    {
        identity.GetComponent<CharacterController>().enabled = false;
        identity.transform.position = pos;
        identity.GetComponent<CharacterController>().enabled = true;
    }
    [Command]
    void MoveToCmd(NetworkIdentity identity, Vector3 pos)
    {
        MoveToRpc(identity, pos);
    }
    #endregion
    #region random role logic
    public void AssignRoles()
    {
        List<GameObject> players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        for (int i = 0; i < GameSettings.numOfImposters; i++)
        {
            int rand = Random.Range(0, players.Count);
            SetImposterRpc(players[rand].GetComponent<NetworkIdentity>());
            players.RemoveAt(rand);
        }
        for (int i = 0; i < players.Count; i++)
            SetCrewmateRpc(players[i].GetComponent<NetworkIdentity>());
    }
    #endregion
    #region imposter logic
    [TargetRpc]
    private void SetImposterRpc(NetworkIdentity target)
    {
        GameObject imposter = target.gameObject;

        PlayerUI playerUI = imposter.GetComponent<PlayerUI>();
        playerUI.map = Instantiate(Resources.Load<GameObject>("Prefabs/Player/GUI/ImposterMap"), imposter.transform.Find("PlayerCanvas"));
        playerUI.map.name = "ImposterMap";

        ImposterStateMachine sm = imposter.AddComponent<ImposterStateMachine>();

        sm.ChangeState(sm.InputDisabledState);

        imposter.GetComponent<PlayerLean>()._sm = sm;
        imposter.AddComponent<PlayerVent>();
        imposter.AddComponent<playerDoors>();
    }
    #endregion
    #region crewmate logic
    [TargetRpc]
    private void SetCrewmateRpc(NetworkIdentity target)
    {
        GameObject crewmate = target.gameObject;

        PlayerUI playerUI = crewmate.GetComponent<PlayerUI>();
        playerUI.map = Instantiate(Resources.Load<GameObject>("Prefabs/Player/GUI/CrewmateMap"), crewmate.transform.Find("PlayerCanvas"));
        playerUI.map.name = "CrewmateMap";

        crewmate.GetComponent<PlayerLean>()._sm = crewmate.AddComponent<CrewmateStateMachine>();
    }
    #endregion
    #region intro animation
    void RollIntro()
    {
        if (isServer)
            RollIntroRpc();
        else
            RollIntroCmd();
    }
    [ClientRpc]
    void RollIntroRpc()
    {
        GameObject.FindGameObjectWithTag("Intro").GetComponent<Animator>().enabled = true;
        if (isServer)
            StartCoroutine(TimerUntilInput());
    }
    [Command]
    void RollIntroCmd()
    {
        RollIntroRpc();
    }
    IEnumerator TimerUntilInput()
    {
        yield return new WaitForSeconds(5);
        EnableInputRpc();
    }

    [ClientRpc]
    void EnableInputRpc()
    {
        PlayerStateMachine sm = NetworkClient.localPlayer.GetComponent<PlayerStateMachine>();
        sm.ChangeState(sm.IdleState);
    }
    #endregion
    #endregion

    #region start game button

    private readonly CancellationTokenSource startGameTaskCts = new CancellationTokenSource();
    private bool startingGame;

    [ClientRpc]
    public void InterruptStartGame()
    {
        if (!startingGame)
            return;

        startGameTaskCts.Cancel();
        TextMeshProUGUI startingGameText = GameObject.FindGameObjectWithTag("LobbyTimer").GetComponent<TextMeshProUGUI>();
        startingGameText.color = Color.white;
        startingGameText.text = "press R to start the game";
        startingGame = false;
    }

    [ClientRpc]
    public void StartGameRpc()
    {
        if (!startingGame)
            StartGame();
    }

    private async void StartGame()
    {
        TextMeshProUGUI startingGameText = GameObject.FindGameObjectWithTag("LobbyTimer").GetComponent<TextMeshProUGUI>();

        startingGameText.color = Color.yellow;
        startingGame = true;
        for (int i = 5; i > 0; i--)
        {
            startingGameText.text = "Game starting in: " + i;
            await Task.Delay(1000, startGameTaskCts.Token);
            if (startGameTaskCts.IsCancellationRequested)
                return;
        }

        if (isServer)
            NetworkManager.singleton.ServerChangeScene("TheSkeld");

        Destroy(NetworkClient.localPlayer.GetComponent<PlayerStateMachine>());
        startingGameText.text = "";
        startingGame = false;
    }
    #endregion
}
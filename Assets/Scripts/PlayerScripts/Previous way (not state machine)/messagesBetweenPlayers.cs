using UnityEngine;
using Mirror;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class messagesBetweenPlayers : NetworkBehaviour
{
    #region ready logic
    public int readyCounter = 0;
    public void readyUp()
    {
        if (isServer)
        {
            NetworkClient.localPlayer.GetComponent<messagesBetweenPlayers>().readyCounter++;
            if (NetworkManager.singleton.numPlayers == readyCounter)
            {
                RandomSpawnPoints();
                AssignRoles();
                RollIntro();

            }
        }
        else
            readyUpCmd();
    }
    [Command]
    void readyUpCmd()
    {
        NetworkClient.localPlayer.GetComponent<messagesBetweenPlayers>().readyCounter++;
        if (NetworkManager.singleton.numPlayers == NetworkClient.localPlayer.GetComponent<messagesBetweenPlayers>().readyCounter)
        {
            RandomSpawnPoints();
            AssignRoles();
            RollIntro();
        }
    }
    #endregion

    #region dont destroy on load
    public void getDoNotDestroyOnLoad()
    {
        if (isServer)
            getDoNotDestroyOnLoadRpc();
        else
            getDoNotDestroyOnLoadCmd();
    }

    [Command]
    private void getDoNotDestroyOnLoadCmd()
    {
        getDoNotDestroyOnLoadRpc();
    }
    [ClientRpc]
    private void getDoNotDestroyOnLoadRpc()
    {
        setDoNotDestroyOnLoad(NetworkClient.localPlayer.gameObject);
    }
    public void setDoNotDestroyOnLoad(GameObject identity)
    {
        if (isServer)
            setDoNotDestroyOnLoadRpc(identity);
        else
            setDoNotDestroyOnLoadCmd(identity);
    }

    [Command]
    private void setDoNotDestroyOnLoadCmd(GameObject identity)
    {
        setDoNotDestroyOnLoadRpc(identity);
    }
    [ClientRpc]
    private void setDoNotDestroyOnLoadRpc(GameObject identity)
    {
        dontDestroyOnLoadManager.dontDestroyOnLoad(identity.gameObject);
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

    public void lockDoor(string door)
    {
        if (isServer)
            lockDoorRpc(door);
        else
            lockDoorsCmd(door);
    }

    [Command]
    private void lockDoorsCmd(string door)
    {
        lockDoorRpc(door);
    }

    [ClientRpc]
    private void lockDoorRpc(string door)
    {
        GameObject.FindWithTag(door).GetComponent<LockDoor>().lockDoor();
    }

    #endregion

    #region set name tags

    public void getNames()
    {
        if (isServer)
            getNamesRpc();
        else
            getNamesCmd();
    }

    [Command]
    private void getNamesCmd()
    {
        getNamesRpc();
    }

    [ClientRpc]
    private void getNamesRpc()
    {
        setNames(NetworkClient.localPlayer.gameObject, nameSingleton.currentName);
    }

    private void setNames(GameObject localPlayer, string playerName)
    {
        if (isServer)
            setNamesRpc(localPlayer, playerName);
        else
            setNamesCmd(localPlayer, playerName);
    }

    [Command]
    private void setNamesCmd(GameObject formerLocalPlayerObj, string playerName)
    {
        setNamesRpc(formerLocalPlayerObj, playerName);
    }

    [ClientRpc]
    private void setNamesRpc(GameObject formerLocalPlayerObj, string playerName)
    {
        bool onlySpaces = true;

        foreach (char c in playerName)
            if (c != ' ')
                onlySpaces = false;

        if (onlySpaces)
            playerName = "Nice try";

        formerLocalPlayerObj.transform.Find("NameTag").GetComponent<TextMeshPro>().text = playerName;
        formerLocalPlayerObj.name = playerName;
    }

    #endregion

    #region set colors
    public void getColors()
    {
        if (isServer)
            getColorsRpc();
        else
            getColorsCmd();
    }

    [Command]
    private void getColorsCmd()
    {
        getColorsRpc();
    }

    [ClientRpc]
    private void getColorsRpc()
    {
        setColors(NetworkClient.localPlayer.gameObject, NetworkClient.localPlayer.GetComponent<playerColor>().currentColor);
    }

    private void setColors(GameObject localPlayer, string color)
    {
        if (isServer)
            setColorsRpc(localPlayer, color);
        else
            setColorsCmd(localPlayer, color);
    }

    [Command]
    private void setColorsCmd(GameObject formerLocalPlayerObj, string color)
    {
        setColorsRpc(formerLocalPlayerObj, color);
    }

    [ClientRpc]
    private void setColorsRpc(GameObject formerLocalPlayerObj, string color)
    {
        if (color != "" && isServer)
            formerLocalPlayerObj.GetComponent<playerColor>().changeColor(color);
    }

    #endregion

    #region join leave messages

    public void joinLeaveMessage(string msg, Color color)
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
        List<GameObject> players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        List<GameObject> spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPoints"));

        foreach (GameObject p in players)
        {
            int rand = Random.Range(0, spawnPoints.Count);
            moveTo(p.GetComponent<NetworkIdentity>(), spawnPoints[rand].transform.position);
            spawnPoints.RemoveAt(rand);
        }
    }
    void moveTo(NetworkIdentity identity, Vector3 pos)
    {
        if (isServer)
            moveToRpc(identity,pos);
        else
            moveToCmd(identity, pos);
    }
    [ClientRpc]
    void moveToRpc(NetworkIdentity identity, Vector3 pos)
    {
        identity.GetComponent<CharacterController>().enabled = false;
        identity.transform.position = pos;
        identity.GetComponent<CharacterController>().enabled = true;
    }
    [Command]
    void moveToCmd(NetworkIdentity identity, Vector3 pos)
    {
        moveToRpc(identity, pos);
    }
    #endregion
    #region random role logic
    public void AssignRoles()
    {
        List<GameObject> players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        for (int i = 0; i < GameSettings.numOfImposters; i++)
        {
            int rand = Random.Range(0, players.Count);
            SetImposterRpc(players[rand].GetComponent<NetworkIdentity>().connectionToClient); // fix this future nigga it's almost 4 am and I don't know what to pass just fuck me
            players.RemoveAt(rand);
        }
        for (int i = 0; i < players.Count; i++)
            SetCrewmateRpc(players[i].GetComponent<NetworkIdentity>().connectionToClient);
    }
    #endregion
    #region imposter logic
    [TargetRpc]
    private void SetImposterRpc(NetworkConnection target)
    {
        //TODO: set imposter logic here
        GameObject imposter = target.identity.gameObject;

        playerUI playerUI = imposter.GetComponent<playerUI>();
        playerUI.map = Instantiate(Resources.Load<GameObject>("Prefabs/Player/GUI/ImposterMap"), imposter.transform.Find("PlayerCanvas"));
        playerUI.map.name = "ImposterMap";

        imposter.GetComponent<playerState>().isImposter = true;
        imposter.AddComponent<playerVent>();
        imposter.AddComponent<playerDoors>();
    }
    #endregion
    #region crewmate logic
    [TargetRpc]
    private void SetCrewmateRpc(NetworkConnection target)
    {
        //TODO: set crewmate logic here 

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
            StartCoroutine(timerUntilInput());
    }
    [Command]
    void RollIntroCmd()
    {
        RollIntroRpc();
    }
    IEnumerator timerUntilInput()
    {
        yield return new WaitForSeconds(5);
        EnableInput();
    }
    void EnableInput()
    {
        if (isServer)
            EnableInputRpc();
        else
            EnableInputCmd();
    }
    [ClientRpc]
    void EnableInputRpc()
    {
        NetworkClient.localPlayer.GetComponent<playerState>().inputDisabled = false;
    }
    [Command]
    void EnableInputCmd()
    {
        EnableInputRpc();
    }
    #endregion
    #endregion

    #region start game button

    private Coroutine currentStartGame;
    private bool startingGame;

    [ClientRpc]
    public void stopStartGame()
    {
        TextMeshProUGUI startingGameText = GameObject.FindGameObjectWithTag("LobbyTimer").GetComponent<TextMeshProUGUI>();

        if (!startingGame)
            return;

        StopCoroutine(currentStartGame);
        startingGameText.color = Color.white;
        startingGameText.text = "press R to start the game";
        startingGame = false;
    }

    [ClientRpc]
    public void startGameRpc()
    {
        if (!startingGame)
            currentStartGame = StartCoroutine(StartGame());
    }
    private IEnumerator StartGame()
    {
        TextMeshProUGUI startingGameText = GameObject.FindGameObjectWithTag("LobbyTimer").GetComponent<TextMeshProUGUI>();

        startingGameText.color = Color.yellow;
        startingGame = true;
        for (int i = 5; i > 0; i--)
        {
            startingGameText.text = "Game starting in: " + i;
            yield return new WaitForSeconds(1);
        }

        if (isServer)
            NetworkManager.singleton.ServerChangeScene("TheSkeld");
        NetworkClient.localPlayer.GetComponent<playerState>().inputDisabled = true;
        startingGameText.text = "";
        startingGame = false;
    }
    #endregion
}
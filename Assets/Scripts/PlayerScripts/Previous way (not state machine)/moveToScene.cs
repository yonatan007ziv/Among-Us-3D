using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class moveToScene : MonoBehaviour
{
    public void MoveToScene(string sceneName)
    {
        if (sceneName == "host" && nameSingleton.currentName != "")
        {
            SceneManager.LoadScene("Lobby");
            GameObject hostTag = new GameObject("hostTag");
            dontDestroyOnLoadManager.dontDestroyOnLoad(hostTag);
            hostTag.tag = "hostTag";
        }
        else if (sceneName == "client" && nameSingleton.currentName != "")
        {
            SceneManager.LoadScene("Client");
            GameObject clientTag = new GameObject("clientTag");
            dontDestroyOnLoadManager.dontDestroyOnLoad(clientTag);
            clientTag.tag = "clientTag";
        }
        else if (sceneName == "clientIp" && NetworkManager.singleton.networkAddress != "")
        {
            SceneManager.LoadScene("Lobby");
        }
        else if (sceneName != "host" && sceneName != "client"&& sceneName != "clientIp")
            SceneManager.LoadScene(sceneName);
    }
}
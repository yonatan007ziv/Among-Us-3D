using Mirror;
using System;
using UnityEngine;
using UnityEngine.UI;

public class StartHost : MonoBehaviour
{
    public Condition condition;

    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(Host);
    }

    public void Host()
    {
        if (condition.Met())
        {
            SceneManager.Instance.LoadScene(SceneManager.LobbySceneName, PrepareHost);
            // Crashes -> 
        }
        else
            Debug.Log("Condition isn't Met");
    }

    private void PrepareHost()
    {
        NetworkManager.singleton.StartHost();
    }
}
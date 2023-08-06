using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    // Singleton Pattern
    public static SceneManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError("Scene Manager Already Exists! Destroying...");
            Destroy(gameObject);
        }
    }

    // Scene Names
    public static readonly string SkeldSceneName = "TheSkeld", LobbySceneName = "Lobby";

    public string CurrentSceneName { get; private set; }



    public void LoadScene(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene, LoadSceneMode.Single);
        CurrentSceneName = scene;
    }

    public void LoadScene(string scene, Action onLoaded)
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += (s, e) => onLoaded();
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene, LoadSceneMode.Single);
        CurrentSceneName = scene;
    }
}
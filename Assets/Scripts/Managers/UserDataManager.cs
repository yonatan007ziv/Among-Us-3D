using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    // Singleton Pattern
    public static UserDataManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError("User Data Manager Already Exists! Destroying...");
            Destroy(gameObject);
        }
    }

    public string Username { get; set; } = "";
    public string Ip { get; set; } = "";
}
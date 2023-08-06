using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class StartClient : MonoBehaviour
{
    public Condition condition;

    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(Host);
    }

    public void Host()
    {

        // fix it's 4 am I am tired
        if (condition.Met())
        {
            NetworkManager.singleton.networkAddress = UserDataManager.Instance.Ip;
            NetworkManager.singleton.StartClient();
        }
    }
}
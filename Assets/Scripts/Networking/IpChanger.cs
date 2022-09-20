using Mirror;
using UnityEngine;

public class IpChanger : MonoBehaviour
{
    public void updateNameString(string newIp)
    {
        NetworkManager.singleton.networkAddress = newIp;
    }
}
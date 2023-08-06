using TMPro;
using UnityEngine;

public class OnUsernameChangedListener : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TMP_InputField>().onValueChanged.AddListener(OnIpChanged);
    }

    public void OnIpChanged(string ip)
    {
        UserDataManager.Instance.Username = ip;
    }
}
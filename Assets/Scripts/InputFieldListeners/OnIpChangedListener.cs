using TMPro;
using UnityEngine;

public class OnIpChangedListener : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TMP_InputField>().onValueChanged.AddListener(OnIpChanged);
    }

    public void OnIpChanged(string ip)
    {
        UserDataManager.Instance.Ip = ip;
    }
}
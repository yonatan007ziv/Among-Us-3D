using UnityEngine;
using UnityEngine.UI;
public class playerDoors : MonoBehaviour
{
    private PlayerMessages msgPlayer;
    private void Start()
    {
        msgPlayer = GetComponent<PlayerMessages>();
        foreach (Transform child in transform.Find("PlayerCanvas/ImposterMap"))
            child.GetComponent<Button>().onClick.AddListener(() => LockDoor(child.name));
    }
    public void LockDoor(string doorName)
    {
        msgPlayer.LockDoor(doorName);
    }
}

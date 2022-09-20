using UnityEngine;
using UnityEngine.UI;
public class playerDoors : MonoBehaviour
{
    private messagesBetweenPlayers msgPlayer;
    private void Start()
    {
        msgPlayer = GetComponent<messagesBetweenPlayers>();
        foreach (Transform child in transform.Find("PlayerCanvas/ImposterMap"))
            child.GetComponent<Button>().onClick.AddListener(() => LockDoor(child.name));
    }
    public void LockDoor(string doorName)
    {
        msgPlayer.lockDoor(doorName);
    }
}

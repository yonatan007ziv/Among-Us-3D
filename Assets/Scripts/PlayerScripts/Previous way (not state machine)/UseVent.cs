using Mirror;

public class UseVent : use
{
    public override void useFunc()
    {
        StartCoroutine(NetworkClient.localPlayer.GetComponent<playerVent>().useVent());
    }
}

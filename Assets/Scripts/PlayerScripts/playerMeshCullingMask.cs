using UnityEngine;
using Mirror;

public class playerMeshCullingMask : NetworkBehaviour
{
    public GameObject astronautMesh;
    public GameObject nameTag;

    void Start()
    {
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }
        astronautMesh.layer = LayerMask.NameToLayer("LocalPlayer");
        nameTag.layer = LayerMask.NameToLayer("LocalPlayer");
    }
}
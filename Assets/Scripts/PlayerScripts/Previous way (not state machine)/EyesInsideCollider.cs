using UnityEngine;
using System.Collections.Generic;

public class EyesInsideCollider : MonoBehaviour
{
    public Collider playerCol;
    private GameObject blackCanvas;
    public List<Collider> currentColliders = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCol || other.isTrigger || other.name == "player")
            return;
        if (currentColliders.Count == 0)
            blackCanvas = Instantiate(Resources.Load<GameObject>("CameraWallFix/blackCanvas"));
        currentColliders.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == playerCol)
            return;
        
        currentColliders.Remove(other);
        if (currentColliders.Count == 0)
            Destroy(blackCanvas);
    }
}
using UnityEngine;
using Mirror;

public class playerRotationHeadSpine : MonoBehaviour
{
    public float leanZ;
    public Transform eyes;
    public Transform head;
    public Transform spine;

    void Update()
    {
        spine.localEulerAngles = new Vector3(angleX() / 2, 0, leanZ);
        head.localEulerAngles = new Vector3(angleX() / 2, 0, 0);
    }

    float angleX()
    {
        if (eyes.localEulerAngles.x >= 270)
            return eyes.localEulerAngles.x - 360;
        else
            return eyes.localEulerAngles.x;
    }
}
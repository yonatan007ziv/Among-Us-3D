using UnityEngine;

public class PlayerRotationHeadSpine : MonoBehaviour
{
    public Transform eyes;
    public Transform head;
    public Transform spine;

    public void UpdateRotation(Transform headTarget, Transform spineTarget, float t)
    {
        Quaternion headAddition = Quaternion.Euler(AngleX(), 0, 0);
        Quaternion spineAddition = Quaternion.Euler(AngleX() / 2, 0, 0);

        head.rotation = Quaternion.Slerp(head.rotation, headTarget.rotation * headAddition, t);
        spine.rotation = Quaternion.Slerp(spine.rotation, spineTarget.rotation * spineAddition, t);
    }

    float AngleX()
    {
        float angle = eyes.localEulerAngles.x;
        if (angle >= 270)
            angle -= 360;
        return angle;
    }
}
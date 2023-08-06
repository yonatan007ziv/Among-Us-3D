using UnityEngine;

public class PlayerLean : MonoBehaviour
{
    public float speed = 2.5f;
    public PlayerRotationHeadSpine playerRotationHeadSpine;
    public PlayerStateMachine _sm;
    public Transform eyesMiddle, headMiddle, spineMiddle;
    public Transform eyesRight, headRight, spineRight;
    public Transform eyesLeft, headLeft, spineLeft;
    public Transform eyes;

    private float timerRight;
    private float timerMiddle;
    private float timerLeft;

    void Update()
    {
        bool keyQ = Input.GetKey(KeyCode.Q), keyE = Input.GetKey(KeyCode.E), hitLeft = HitLeft(), hitRight = HitRight();

        if (!(keyQ != keyE) || keyQ && hitLeft || keyE && hitRight || _sm.CurrentState is InputDisabledBase)
        {
            timerMiddle += Time.deltaTime * speed;
            UpdateLeanRotation(eyesMiddle,headMiddle, spineMiddle, timerMiddle);

            timerRight = 0;
            timerLeft = 0;
        }
        else if (keyQ && !hitLeft)
        {
            timerLeft += Time.deltaTime * speed;
            UpdateLeanRotation(eyesLeft, headLeft, spineLeft, timerLeft);

            timerRight = 0;
            timerMiddle = 0;
        }
        else if (keyE && !hitRight)
        {
            timerRight += Time.deltaTime * speed;
            UpdateLeanRotation(eyesRight, headRight, spineRight, timerRight);

            timerLeft = 0;
            timerMiddle = 0;
        }
    }

    private void UpdateLeanRotation(Transform eyesTarget, Transform headTarget, Transform spineTarget, float t)
    {
        playerRotationHeadSpine.UpdateRotation(headTarget, spineTarget, t * speed);
        eyes.SetPositionAndRotation(Vector3.Lerp(eyes.position, eyesTarget.position, t * speed), Quaternion.Slerp(eyes.rotation, eyesTarget.rotation, t * speed));
    }

    private bool HitRight()
    {
        return Physics.Raycast(eyesMiddle.position, eyesMiddle.right, 2, ~LayerMask.GetMask("LocalPlayer"));
    }

    private bool HitLeft()
    {
        return Physics.Raycast(eyesMiddle.position, -eyesMiddle.right, 2, ~LayerMask.GetMask("LocalPlayer"));
    }
}
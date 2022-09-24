using UnityEngine;
using Mirror;

public class playerLean : MonoBehaviour
{
    public float speed = 2.5f;
    public Vector3 totalRot;
    public playerRotationHeadSpine playerRotationHeadSpine;
    public playerState _sm;
    public Transform player;
    public Transform eyesMiddle;
    public Transform eyesRight;
    public Transform eyesLeft;
    public Transform eyes;
    private float timerRight;
    private float timerNone;
    private float timerLeft;
    public float rotationSpeedMultiplier = 2;
    public bool inVent;
    
    void Update()
    {
        Vector3 localEulerAnglesEyes = eyes.localEulerAngles;
        if (inVent)
        {
            timerNone += Time.deltaTime * speed;

            playerRotationHeadSpine.leanZ = Mathf.Lerp(playerRotationHeadSpine.leanZ, 0, timerNone);

            totalRot = Quaternion.Slerp(Quaternion.Euler(localEulerAnglesEyes),
                Quaternion.Euler(new Vector3(localEulerAnglesEyes.x, localEulerAnglesEyes.y,
                    eyesMiddle.localEulerAngles.z)), timerNone * rotationSpeedMultiplier).eulerAngles;

            timerRight = 0;
            timerLeft = 0;
            return;
        }

        if (_sm.inputDisabled)
        {
            timerNone += Time.deltaTime * speed;

            playerRotationHeadSpine.leanZ = Mathf.Lerp(playerRotationHeadSpine.leanZ, 0, timerNone);

            eyes.position = Vector3.Lerp(eyes.position, eyesMiddle.position, timerNone);

            totalRot = Quaternion.Slerp(Quaternion.Euler(localEulerAnglesEyes),
                Quaternion.Euler(new Vector3(localEulerAnglesEyes.x, localEulerAnglesEyes.y,
                    eyesMiddle.localEulerAngles.z)), timerNone * rotationSpeedMultiplier).eulerAngles;

            timerRight = 0;
            timerLeft = 0;
            return;
        }
        
        if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Q))
        {
            timerNone += Time.deltaTime * speed;

            playerRotationHeadSpine.leanZ = Mathf.Lerp(playerRotationHeadSpine.leanZ, 0, timerNone);

            eyes.position = Vector3.Lerp(eyes.position, eyesMiddle.position, timerNone);

            totalRot = Quaternion.Slerp(Quaternion.Euler(localEulerAnglesEyes),
                Quaternion.Euler(new Vector3(localEulerAnglesEyes.x, localEulerAnglesEyes.y,
                    eyesMiddle.localEulerAngles.z)), timerNone * rotationSpeedMultiplier).eulerAngles;

            timerRight = 0;
            timerLeft = 0;
        }
        else if (Input.GetKey(KeyCode.E) && !Physics.Raycast(eyesMiddle.position, player.right, 2.25f, ~LayerMask.GetMask("LocalPlayer")))
        {
            timerRight += Time.deltaTime * speed;

            playerRotationHeadSpine.leanZ = Mathf.Lerp(playerRotationHeadSpine.leanZ, -37.5f, timerRight);

            eyes.position = Vector3.Lerp(eyes.position, eyesRight.position, timerRight);

            totalRot = Quaternion.Slerp(Quaternion.Euler(localEulerAnglesEyes),
                Quaternion.Euler(new Vector3(localEulerAnglesEyes.x, localEulerAnglesEyes.y,
                    eyesRight.localEulerAngles.z)), timerRight * rotationSpeedMultiplier).eulerAngles;

            timerLeft = 0;
            timerNone = 0;
        }
        else if (Input.GetKey(KeyCode.Q) && !Physics.Raycast(eyesMiddle.position, -player.right, 2.25f, ~LayerMask.GetMask("LocalPlayer")))
        {
            timerLeft += Time.deltaTime * speed;

            playerRotationHeadSpine.leanZ = Mathf.Lerp(playerRotationHeadSpine.leanZ, 37.5f, timerLeft);

            eyes.position = Vector3.Lerp(eyes.position, eyesLeft.position, timerLeft);

            totalRot = Quaternion.Slerp(Quaternion.Euler(localEulerAnglesEyes),
                Quaternion.Euler(new Vector3(localEulerAnglesEyes.x, localEulerAnglesEyes.y,
                    eyesLeft.localEulerAngles.z)), timerLeft * rotationSpeedMultiplier).eulerAngles;

            timerRight = 0;
            timerNone = 0;
        }
        else
        {
            timerNone += Time.deltaTime * speed;

            playerRotationHeadSpine.leanZ = Mathf.Lerp(playerRotationHeadSpine.leanZ, 0, timerNone);

            eyes.position = Vector3.Lerp(eyes.position, eyesMiddle.position, timerNone);

            totalRot = Quaternion.Slerp(Quaternion.Euler(localEulerAnglesEyes),
                Quaternion.Euler(new Vector3(localEulerAnglesEyes.x, localEulerAnglesEyes.y,
                    eyesMiddle.localEulerAngles.z)), timerNone * rotationSpeedMultiplier).eulerAngles;

            timerRight = 0;
            timerLeft = 0;
        }
    }
}
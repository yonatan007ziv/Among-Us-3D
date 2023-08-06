using UnityEngine;
using Mirror;

public class PlayerLook : MonoBehaviour
{
    public Transform player;
    public Transform eyes;
    public PlayerLean playerLean;
    [Range(1, 100)] public int sensitivity = 50;
    public float xRot,zRot;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            return;
        
        float mouseX = Input.GetAxis("Mouse X") * sensitivity / 10;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity / 10;
        
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -85f, 85f);

        player.Rotate(Vector3.up * mouseX);
        eyes.localRotation = Quaternion.Euler(xRot, 0, eyes.localRotation.eulerAngles.z);
    }
}
/*
using UnityEngine;
using Mirror;

public class playerMovement : NetworkBehaviour
{
    public bool inputDisabled;
    public Transform groundCheckPos;
    public CharacterController cController;
    private int speed = 10;
    private int jumpHeight = 10;
    private Vector3 gravityMotion = Vector3.zero;

    private void Start()
    {
        if (!isLocalPlayer)
            Destroy(this);
    }

    void Update()
    {
        bool isGroundedLocal = cController.isGrounded;

        if (inputDisabled)
        {
            if (isGroundedLocal)
                gravityMotion.y = -9.81f;
            else
                gravityMotion.y -= 9.81f * 2.5f * Time.deltaTime;
            cController.Move(gravityMotion * Time.deltaTime);
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (isGroundedLocal)
        {
            gravityMotion.y = -9.81f;
            if (Input.GetButton("Jump"))
            {

                gravityMotion.y = jumpHeight;

            }
        }
        else
        {
            gravityMotion.y -= 9.81f * 2.5f * Time.deltaTime;
        }

        Vector3 movement =
            horizontal * transform.right + vertical * transform.forward;
        movement =
            Vector3.Normalize(movement);
        cController.Move(movement * speed * Time.deltaTime);

        cController.Move(gravityMotion * Time.deltaTime); //gravity, do not touch
    }

    //not used, replaced by cController.isGrounded
    private bool isGrounded()
    {
        return Physics.CheckCapsule(groundCheckPos.position + Vector3.forward - Vector3.forward / 5,
                   groundCheckPos.position + Vector3.back - Vector3.back / 5, 0.2f)
               || Physics.CheckCapsule(groundCheckPos.position + Vector3.right - Vector3.right / 5,
                   groundCheckPos.position + Vector3.left - Vector3.left / 5,
                   0.2f);
    }


    //ground checks draw
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheckPos.position + Vector3.forward - Vector3.forward / 5, 0.2f);
        Gizmos.DrawSphere(groundCheckPos.position + Vector3.back - Vector3.back / 5, 0.2f);
        Gizmos.DrawSphere(groundCheckPos.position + Vector3.right - Vector3.right / 5, 0.2f);
        Gizmos.DrawSphere(groundCheckPos.position + Vector3.left - Vector3.left / 5, 0.2f);
    }
}
*/
using UnityEngine;

public class GroundCeilingChecking : MonoBehaviour
{
    public bool isCeilinged;
    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform ceilingCheck, groundCheck;

    private void FixedUpdate()
    {
        isCeilinged = Physics.CheckBox(ceilingCheck.position, new Vector3(1, 0.2f, 1) / 2, ceilingCheck.localRotation, groundLayer);
        isGrounded = Physics.CheckBox(groundCheck.position, new Vector3(1, 0.2f, 1) / 2, groundCheck.localRotation, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(ceilingCheck.position, 0.25f);
        Gizmos.DrawSphere(groundCheck.position, 0.25f);
    }
}
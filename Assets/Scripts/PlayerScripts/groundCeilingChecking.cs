using UnityEngine;

public class groundCeilingChecking : MonoBehaviour
{
    public bool isCeilinged;
    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform ceilingCheck, groundCheck;

    void Update()
    {
        isCeilinged = Physics.CheckBox(ceilingCheck.position, new Vector3(1, 0.2f, 1) / 2, ceilingCheck.localRotation, groundLayer);
        isGrounded = Physics.CheckBox(groundCheck.position, new Vector3(1, 0.2f, 1) / 2, groundCheck.localRotation, groundLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(ceilingCheck.position, new Vector3(1, 0.2f, 1));
        Gizmos.DrawWireCube(groundCheck.position, new Vector3(1, 0.2f, 1));
    }
}
using UnityEngine;
using Mirror;

public class playerMovement : NetworkBehaviour
{
    public Transform player;
    public bool inputDisabled;
    public CharacterController cController;
    private int speed = 10;
    private float airSpeed = 25;
    private int jumpHeight = 10;
    private Vector3 gravityMotion = Vector3.zero;
    public Vector3 movement = Vector3.zero;
    public Vector3 airMovement = Vector3.zero;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }
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
            movement = Vector3.ClampMagnitude(horizontal * player.right + vertical * player.forward, 1);
            cController.Move(movement * speed * Time.deltaTime);
            
            gravityMotion.y = -9.81f;
            
            if (Input.GetButton("Jump"))
                gravityMotion.y = jumpHeight;
            airMovement = Vector3.zero;
        }
        else
        {
            horizontal /= airSpeed;
            vertical /= airSpeed;

            airMovement += Vector3.ClampMagnitude(horizontal * player.right + vertical * player.forward, 0.5f)/2;
            cController.Move(airMovement * Time.deltaTime + movement * speed * 0.5f * Time.deltaTime);

            gravityMotion.y -= 9.81f * 2.5f * Time.deltaTime;
        }
        
        cController.Move(gravityMotion * Time.deltaTime);
    }
}
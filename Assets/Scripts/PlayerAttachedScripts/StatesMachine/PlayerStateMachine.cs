using System;
using UnityEngine;

public abstract class PlayerStateMachine : StateMachine
{
    protected CharacterController cC;
    protected Animator anim;
    protected GroundCeilingChecking gcC;

    // Shared States
    public IdleBase IdleState { get; protected set; }
    public InputDisabledBase InputDisabledState { get; protected set; }

    public Vector3 airMovementVector;
    public Vector3 movementVector;
    public float velocityY;
    public float airInputTimer = 1;

    public const float gravity = -9.81f * 3f;
    public const float speed = 10;
    public const float jumpHeight = 2.5f;

    public float horizontalInput;
    public float verticalInput;

    public void PlayAnimation(string animation)
    {
        anim.Play(animation);
    }

    public bool IsCeilinged()
    {
        return gcC.isCeilinged;
    }

    public bool IsGrounded()
    {
        return gcC.isGrounded;
    }

    public bool IsFalling()
    {
        return cC.velocity.y < -Mathf.Epsilon;
    }

    public void AxisInput(float multiplier)
    {
        horizontalInput = Input.GetAxisRaw("Horizontal") * multiplier;
        verticalInput = Input.GetAxisRaw("Vertical") * multiplier;
    }

    public void PhysicsUpdate()
    {
        velocityY += gravity * Time.deltaTime;

        if (!gcC.isGrounded)
        {
            airMovementVector += Vector3.ClampMagnitude(transform.right * horizontalInput + transform.forward * verticalInput, 1) * Time.deltaTime * speed;
            airMovementVector.y = 0;
            airMovementVector = Vector3.ClampMagnitude(airMovementVector, speed);
            airMovementVector.y = velocityY;
            cC.Move(airMovementVector * Time.deltaTime);
            return;
        }

        movementVector = Vector3.ClampMagnitude(transform.right * horizontalInput + transform.forward * verticalInput, 1) * speed;
        airMovementVector = movementVector;

        if (gcC.isGrounded && velocityY <= Mathf.Epsilon)
        {
            airInputTimer = 1;
            velocityY = 0;
        }

        movementVector.y = velocityY;
        cC.Move(movementVector * Time.deltaTime);
    }
}
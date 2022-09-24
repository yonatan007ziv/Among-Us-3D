using UnityEngine;
using Mirror;

public class playerState : StateMachine
{
    public bool isImposter;

    public Vector3 airMovementVector;
    public Vector3 movementVector;
    public string currentStateName;
    public float speedMultiplier = 1;
    public float jumpHeight = 2.5f;
    public float velocityY;
    public float airInputTimer = 1;

    public float horizontalInput;
    public float verticalInput;

    public CharacterController cC;
    public Animator anim;
    public groundCeilingChecking gcC;

    private bool _inputDisabled;
    public bool inputDisabled
    {
        get 
        { 
            return _inputDisabled; 
        }
        set
        {
            horizontalInput = 0;
            verticalInput = 0;
            _inputDisabled = value;
        }
    }

    //States
    [HideInInspector]
    public Idle idleState;
    [HideInInspector]
    public Run runState;
    [HideInInspector]
    public Walk walkState;
    [HideInInspector]
    public Jump jumpState;
    [HideInInspector]
    public Fall fallState;
    [HideInInspector]
    public RunNoTransition runNoTransitionState;

    public readonly float gravity = -9.81f * 3f;
    public float speed = 10;

    private void Awake()
    {
        cC = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        gcC = GetComponent<groundCeilingChecking>();
        idleState = new Idle(this);
        runState = new Run(this);
        walkState = new Walk(this);
        jumpState = new Jump(this);
        fallState = new Fall(this);
        runNoTransitionState = new RunNoTransition(this);
    }

    protected override BaseState getInitialState()
    {
        return idleState;
    }

    public void stateName()
    {
        currentStateName = currentState.name;
    }

    public void updatePhysics()
    {
        velocityY += gravity * Time.deltaTime;

        if (!gcC.isGrounded)
        {
            airMovementVector += Vector3.ClampMagnitude(transform.right * horizontalInput + transform.forward * verticalInput, 1) * Time.deltaTime * speed * speedMultiplier * 3;
            airMovementVector.y = 0;
            airMovementVector = Vector3.ClampMagnitude(airMovementVector, speed * speedMultiplier);
            airMovementVector.y = velocityY;
            cC.Move(airMovementVector * Time.deltaTime);
            return;
        }

        movementVector = Vector3.ClampMagnitude(transform.right * horizontalInput + transform.forward * verticalInput, 1) * speed * speedMultiplier;
        airMovementVector = movementVector;

        if (gcC.isGrounded && velocityY <= Mathf.Epsilon)
        {
            airInputTimer = 1;
            velocityY = 0;
        }

        movementVector.y = velocityY;
        cC.Move(movementVector * Time.deltaTime);
    }
    public void takeAxisInput(float inputDivisor)
    {
        if (inputDisabled)
        {
            horizontalInput = 0;
            verticalInput = 0;
            return;
        }
        horizontalInput = Input.GetAxisRaw("Horizontal") / inputDivisor;
        verticalInput = Input.GetAxisRaw("Vertical") / inputDivisor;
    }
}
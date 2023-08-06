using UnityEngine;

public class UnassignedStateMachine : PlayerStateMachine
{
    // States
    [HideInInspector]
    public IdleUnassigned idleState;
    [HideInInspector]
    public RunUnassigned runState;
    [HideInInspector]
    public WalkUnassigned walkState;
    [HideInInspector]
    public JumpUnassigned jumpState;
    [HideInInspector]
    public FallUnassigned fallState;
    [HideInInspector]
    public InputDisabledUnassigned inputDisabledState;

    private void Awake()
    {
        cC = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        gcC = GetComponent<GroundCeilingChecking>();

        // Shared States
        idleState = new IdleUnassigned(this);
        inputDisabledState = new InputDisabledUnassigned(this);
        IdleState = idleState;
        InputDisabledState = inputDisabledState;

        runState = new RunUnassigned(this);
        walkState = new WalkUnassigned(this);
        jumpState = new JumpUnassigned(this);
        fallState = new FallUnassigned(this);
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}
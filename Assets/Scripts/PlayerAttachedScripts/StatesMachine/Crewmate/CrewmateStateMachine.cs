using UnityEngine;

public class CrewmateStateMachine : PlayerStateMachine
{
    // States
    [HideInInspector]
    public IdleCrewmate idleState;
    [HideInInspector]
    public RunCrewmate runState;
    [HideInInspector]
    public WalkCrewmate walkState;
    [HideInInspector]
    public JumpCrewmate jumpState;
    [HideInInspector]
    public FallCrewmate fallState;
    [HideInInspector]
    public InputDisabledCrewmate inputDisabledState;

    private void Awake()
    {
        cC = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        gcC = GetComponent<GroundCeilingChecking>();

        // Shared States
        idleState = new IdleCrewmate(this);
        inputDisabledState = new InputDisabledCrewmate(this);
        IdleState = idleState;
        InputDisabledState = inputDisabledState;

        runState = new RunCrewmate(this);
        walkState = new WalkCrewmate(this);
        jumpState = new JumpCrewmate(this);
        fallState = new FallCrewmate(this);
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}
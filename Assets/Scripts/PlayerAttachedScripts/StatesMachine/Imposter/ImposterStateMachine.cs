using UnityEngine;

public class ImposterStateMachine : PlayerStateMachine
{
    // Shared States

    //States
    [HideInInspector]
    public RunImposter runState;
    [HideInInspector]
    public WalkImposter walkState;
    [HideInInspector]
    public JumpImposter jumpState;
    [HideInInspector]
    public FallImposter fallState;

    private void Awake()
    {
        cC = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        gcC = GetComponent<GroundCeilingChecking>();

        // Shared States
        IdleState = new IdleImposter(this);
        InputDisabledState = new InputDisabledImposter(this);

        runState = new RunImposter(this);
        walkState = new WalkImposter(this);
        jumpState = new JumpImposter(this);
        fallState = new FallImposter(this);
    }

    protected override BaseState GetInitialState()
    {
        return IdleState;
    }
}
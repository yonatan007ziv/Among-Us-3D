using UnityEngine;

public class InputDisabledImposter : InputDisabledBase
{
    private readonly ImposterStateMachine _sm;

    public InputDisabledImposter(ImposterStateMachine stateMachine) : base(stateMachine) { _sm = stateMachine; }

    public override void Enter()
    {
        base.Enter();
        _sm.PlayAnimation("Idle");

        _sm.horizontalInput = 0;
        _sm.verticalInput = 0;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        Debug.Log("sdf");
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _sm.PhysicsUpdate();
    }
}
using UnityEngine;

public class FallImposter : BaseState
{
    private readonly ImposterStateMachine _sm;

    public FallImposter(ImposterStateMachine stateMachine) : base("Fall", stateMachine) { _sm = stateMachine; }

    public override void Enter()
    {
        base.Enter();
        _sm.PlayAnimation("Fall");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _sm.AxisInput(_sm.airInputTimer);
        _sm.airInputTimer += Time.deltaTime;

        if (_sm.IsGrounded())
            _sm.ChangeState(_sm.IdleState);
    }

    public override void UpdatePhysics()
    {
        _sm.PhysicsUpdate();
        base.UpdatePhysics();
    }
}
using UnityEngine;

public class JumpUnassigned : BaseState
{
    private readonly UnassignedStateMachine _sm;

    public JumpUnassigned(UnassignedStateMachine stateMachine) : base("Jump", stateMachine) { _sm = stateMachine; }

    public override void Enter()
    {
        base.Enter();
        _sm.PlayAnimation("Jump");
        _sm.velocityY = Mathf.Sqrt(PlayerStateMachine.jumpHeight * -2 * PlayerStateMachine.gravity);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _sm.AxisInput(1 / _sm.airInputTimer);
        _sm.airInputTimer += Time.deltaTime;

        // isCeilinged ?
        if (_sm.IsCeilinged())
            _sm.velocityY = 0;

        // isFalling ?
        else if (_sm.IsFalling())
            _sm.ChangeState(_sm.fallState);
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _sm.PhysicsUpdate();
    }
}
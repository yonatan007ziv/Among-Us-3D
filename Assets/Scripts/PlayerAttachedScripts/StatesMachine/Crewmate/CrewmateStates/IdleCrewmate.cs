using UnityEngine;

public class IdleCrewmate : IdleBase
{
    private readonly CrewmateStateMachine _sm;

    public IdleCrewmate(CrewmateStateMachine stateMachine) : base(stateMachine) { _sm = stateMachine; }

    public override void Enter()
    {
        base.Enter();
        _sm.PlayAnimation("Idle");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _sm.AxisInput(1);

        // isRunning ?
        if (_sm.horizontalInput != 0 || _sm.verticalInput != 0)
            _sm.ChangeState(_sm.runState);

        if (Input.GetKeyDown(KeyCode.Space) && _sm.IsGrounded())
            _sm.ChangeState(_sm.jumpState);
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _sm.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        _sm.airMovementVector = _sm.movementVector;
    }
}
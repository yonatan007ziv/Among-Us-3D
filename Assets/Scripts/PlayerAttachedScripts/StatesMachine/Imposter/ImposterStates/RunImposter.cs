using UnityEngine;

public class RunImposter : BaseState
{
    private readonly ImposterStateMachine _sm;

    public RunImposter(ImposterStateMachine stateMachine) : base("Run", stateMachine) { _sm = stateMachine; }

    public override void Enter()
    {
        base.Enter();
        _sm.PlayAnimation("Run");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _sm.AxisInput(1);

        // Not Moving ?
        if (_sm.horizontalInput == 0 && _sm.verticalInput == 0)
            _sm.ChangeState(_sm.IdleState);

        // Tried Jumping ?
        else if (Input.GetKeyDown(KeyCode.Space) && _sm.IsGrounded())
            _sm.ChangeState(_sm.jumpState);
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _sm.PhysicsUpdate();
    }
}
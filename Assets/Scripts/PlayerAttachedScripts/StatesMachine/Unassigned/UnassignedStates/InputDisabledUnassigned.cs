public class InputDisabledUnassigned : InputDisabledBase
{
    private readonly UnassignedStateMachine _sm;

    public InputDisabledUnassigned(UnassignedStateMachine stateMachine) : base(stateMachine) { _sm = stateMachine; }

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
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _sm.PhysicsUpdate();
    }
}
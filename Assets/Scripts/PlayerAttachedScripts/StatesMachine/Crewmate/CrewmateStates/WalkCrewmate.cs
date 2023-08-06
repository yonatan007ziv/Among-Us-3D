public class WalkCrewmate : BaseState
{
    private readonly CrewmateStateMachine _sm;

    public WalkCrewmate(CrewmateStateMachine stateMachine) : base("Walk", stateMachine) { _sm = stateMachine; }

    public override void Enter()
    {
        base.Enter();
        _sm.PlayAnimation("Walk");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _sm.AxisInput(2);
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _sm.PhysicsUpdate();
    }
}
using UnityEngine;
public class RunNoTransition : BaseState
{
    private playerState _sm;
    public RunNoTransition(playerState stateMachine) : base("RunNoTransition", stateMachine) { _sm = (playerState)stateMachine; }
    public override void Enter()
    {
        base.Enter();
        _sm.anim.Play("Run");
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _sm.stateName();
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _sm.updatePhysics();
    }
}
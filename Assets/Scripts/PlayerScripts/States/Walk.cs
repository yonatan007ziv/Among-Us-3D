using UnityEngine;
public class Walk : BaseState
{
    private playerState _sm;
    public Walk(playerState stateMachine) : base("Walk", stateMachine) { _sm = (playerState)stateMachine; }
    public override void Enter()
    {
        base.Enter();
        _sm.anim.Play("Walk");
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _sm.takeAxisInput(2);
        _sm.stateName();
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _sm.updatePhysics();
    }
}
using UnityEngine;

public class EnemyCrouchState : EnemyBaseState
{
    public override StateID CurrentStateID { get; set; }
    public override StateID PreviousStateID { get; set; }

    public EnemyCrouchState(EnemyStateMachine stateMachine, StateID previousStateID) : base(stateMachine, previousStateID)
    {
        this.stateMachine = stateMachine;
        this.CurrentStateID = StateID.Crouch;
        this.PreviousStateID = previousStateID;
    }

    public override void Enter()
    {
        base.Enter();   
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedTick()
    {

    }

    public override void LateTick()
    {
        base.LateTick();
    }

    public override void Tick(float deltaTime)
    {
        
    }
 
}

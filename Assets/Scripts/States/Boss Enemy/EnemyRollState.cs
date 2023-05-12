using UnityEngine;
public class EnemyRollState : EnemyBaseState
{
    public override StateID CurrentStateID { get; set; }
    public override StateID PreviousStateID { get; set; }
    private Vector3 rollDirection = new Vector3();
    public EnemyRollState(EnemyStateMachine stateMachine, StateID previousStateID) : base(stateMachine, previousStateID)
    {
        this.stateMachine = stateMachine;
        this.CurrentStateID = StateID.Roll;
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

    }

    public override void Tick(float deltaTime)
    {
       
    }
}

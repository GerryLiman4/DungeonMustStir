public class EnemyFallState : EnemyBaseState
{
    public EnemyFallState(EnemyStateMachine stateMachine, StateID previousStateID) : base(stateMachine, previousStateID)
    {
        this.stateMachine = stateMachine;
        this.PreviousStateID = previousStateID;
        this.CurrentStateID = StateID.Fall;
    }

    public override StateID CurrentStateID { get; set; }
    public override StateID PreviousStateID { get; set; }

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
        base.Tick(deltaTime);
    }
}

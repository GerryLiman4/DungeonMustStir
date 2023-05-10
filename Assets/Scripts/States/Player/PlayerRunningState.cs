//using UnityEngine;

//public class PlayerRunningState : PlayerBaseState
//{
//    private readonly int RunHash = Animator.StringToHash("Run");
//    private const float CrossFadeDuration = 0.1f;

//    public PlayerRunningState(PlayerStateMachine stateMachine, StateID previousStateID) : base(stateMachine,previousStateID)
//    {
//        this.stateMachine = stateMachine;
//        this.PreviousStateID = previousStateID;
//    }

//    public override StateID CurrentStateID { get; set; }
//    public override StateID PreviousStateID { get; set; }

//    public override void Enter()
//    {
//        base.Enter();
//        stateMachine.animator.CrossFadeInFixedTime(RunHash, 0);
//        CurrentStateID = StateID.Run;
//    }

//    public override void Exit()
//    {
//        base.Exit();
//    }

//    public override void FixedTick()
//    {

//    }

//    public override void LateTick()
//    {
//        base.LateTick();
//    }

//    public override void Tick(float deltaTime)
//    {
//        stateMachine.groundDetector.CheckGround();
//        stateMachine.playerMovementController.Move(stateMachine.playerInputReader.movement,true);

//        if (!stateMachine.playerInputReader.isSprinting)
//        {
//            stateMachine.SwitchState(new PlayerWalkingState(this.stateMachine,CurrentStateID));
//        }
//    }

//    protected override void Flip(Vector2 direction)
//    {
//        if (direction.x > 0 && stateMachine.transform.localScale.x > 0) return;
//        else if (direction.x < 0 && stateMachine.transform.localScale.x < 0) return;
//        else if (direction.x == 0) return;

//        stateMachine.transform.localScale = new Vector3(-stateMachine.transform.localScale.x, stateMachine.transform.localScale.y, stateMachine.transform.localScale.z);
//    }

//    protected override void OnJump()
//    {
//        stateMachine.SwitchState(new PlayerJumpState(this.stateMachine, CurrentStateID));
//    }

//    protected override void OnMove(Vector2 direction)
//    {
//        Flip(stateMachine.playerInputReader.movement);
//    }

//    protected override void OnStop()
//    {
//        stateMachine.SwitchState(new PlayerIdleState(this.stateMachine,CurrentStateID));
//    }

//    protected override void OnSprint()
//    {

//    }

//    protected override void OnCrouch()
//    {
//        stateMachine.SwitchState(new PlayerCrouchState(this.stateMachine, CurrentStateID));
//    }
//    protected override void OnAttack()
//    {
//        stateMachine.SwitchState(new PlayerAttackState(this.stateMachine, CurrentStateID));
//    }
//}

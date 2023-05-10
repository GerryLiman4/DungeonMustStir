//using UnityEngine;

//public class PlayerJumpState : PlayerBaseState
//{
//    private readonly int JumpHash = Animator.StringToHash("Jump");
//    private const float CrossFadeDuration = 0.1f;
//    private float jumpTimer;
//    private float jumpTime;
   
//    public PlayerJumpState(PlayerStateMachine stateMachine, StateID previousStateID) : base(stateMachine, previousStateID)
//    {
//        this.stateMachine = stateMachine;
//        this.PreviousStateID = previousStateID;
//    }

//    public override StateID CurrentStateID { get; set; }
//    public override StateID PreviousStateID { get; set; }

//    public override void Enter()
//    {
//        base.Enter();
//        stateMachine.animator.CrossFadeInFixedTime(JumpHash, 0);
//        jumpTime = stateMachine.playerMovementController.jumpTime;
//        jumpTimer = 0;
//        CurrentStateID = StateID.Jump;

//    }

//    public override void Exit()
//    {
//        base.Exit();
//    }

//    public override void FixedTick()
//    {
//        if (stateMachine.playerRigidbody.velocity.y >= 0) return;
//        stateMachine.SwitchState(new PlayerFallState(this.stateMachine, CurrentStateID, PreviousStateID == StateID.Run ? true : false));
//    }

//    public override void LateTick()
//    {
//        // if (stateMachine.playerInputReader.movement == Vector2.zero) stateMachine.SwitchState(new PlayerIdleState(this.stateMachine));
//    }

//    public override void Tick(float deltaTime)
//    {
//        stateMachine.groundDetector.CheckGround();
//        if (PreviousStateID != StateID.Run)
//        {
//            stateMachine.playerMovementController.Move(stateMachine.playerInputReader.movement);
//        }
//        else
//        {
//            stateMachine.playerMovementController.Move(stateMachine.playerInputReader.movement, true);
//        }

//        if (!stateMachine.playerInputReader.isJumping && jumpTimer < stateMachine.playerMovementController.maxHoldTime)
//        {
//            stateMachine.SwitchState(new PlayerFallState(this.stateMachine, CurrentStateID, PreviousStateID == StateID.Run ? true : false)) ;
//            return;
//        }
//        jumpTimer += Time.deltaTime;
        
//        if (jumpTimer > jumpTime) return;
//        stateMachine.playerMovementController.Jump(jumpTimer);
        
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

//    }

//    protected override void OnMove(Vector2 direction)
//    {
//        stateMachine.playerMovementController.Move(stateMachine.playerInputReader.movement);
//        Flip(stateMachine.playerInputReader.movement);
//    }

//    protected override void OnStop()
//    {

//    }

//    protected override void OnSprint()
//    {
        
//    }

//    protected override void OnCrouch()
//    {
       
//    }
//    protected override void OnAttack()
//    {
//        return;
//        Debug.Log("masuk" + PreviousStateID);
//        //stateMachine.SwitchState(new PlayerAttackState(this.stateMachine, CurrentStateID));
//    }
//}

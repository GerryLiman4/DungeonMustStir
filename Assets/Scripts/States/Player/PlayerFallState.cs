//using UnityEngine;

//public class PlayerFallState : PlayerBaseState
//{
//    private readonly int FallHash = Animator.StringToHash("Fall");
//    private const float CrossFadeDuration = 0.1f;
//    private float jumpTimer;
//    private bool isRunning = false;
//    public PlayerFallState(PlayerStateMachine stateMachine, StateID previousStateID) : base(stateMachine,previousStateID)
//    {
//        this.stateMachine = stateMachine;
//        this.PreviousStateID = previousStateID;
//    }

//    public PlayerFallState(PlayerStateMachine stateMachine, StateID previousStateID,bool isRunning) : base(stateMachine, previousStateID)
//    {
//        this.stateMachine = stateMachine;
//        this.PreviousStateID = previousStateID;
//        this.isRunning = isRunning;

//    }

//    public override StateID CurrentStateID { get; set; }
//    public override StateID PreviousStateID { get; set; }

//    public override void Enter()
//    {
//        base.Enter();
//        CurrentStateID = StateID.Fall;
//        stateMachine.animator.CrossFadeInFixedTime(FallHash, 0);
//        stateMachine.playerMovementController.Fall();
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
        
//    }

//    public override void Tick(float deltaTime)
//    {
//        if (!isRunning)
//        {
//            base.Tick(deltaTime);
//        }
//        else
//        {
//            stateMachine.groundDetector.CheckGround();
//            stateMachine.playerMovementController.Move(stateMachine.playerInputReader.movement, true);
//        }
        
//        if (stateMachine.playerRigidbody.velocity.y == 0 && stateMachine.groundDetector.isGrounded)
//        {
//            Debug.Log("knp masuk sini" + stateMachine.groundDetector.isGrounded);
//            stateMachine.SwitchState(new PlayerIdleState(this.stateMachine, CurrentStateID));
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
        
//    }

//    protected override void OnMove(Vector2 direction)
//    {
//        Flip(direction);
//        stateMachine.playerMovementController.Move(direction);
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
//    }
//}

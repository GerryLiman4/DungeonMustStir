using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public StarterAssetsInputs playerInputReader { get; private set; }
    // Input Reader replacement
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    [field: SerializeField] public PlayerInput playerInput { get; private set; }
#endif
    [field: SerializeField] public Animator animator { get; private set; }
    //[field: SerializeField] public Rigidbody2D playerRigidbody { get; private set; }
    //[field: SerializeField] public BoxCollider2D playerBoxCollider{ get; private set; }
    [field: SerializeField] public PlayerMovementController playerMovementController { get; private set; }

    [field: SerializeField] public CharacterController characterController { get; private set; }
    [field: SerializeField] public PlayerConfiguration playerConfiguration { get; private set; }
    //[field: SerializeField] public GroundDetector groundDetector{ get; private set; }
    //[field: SerializeField] public AttackController attackController { get; private set; }
    private GameObject _mainCamera;

    // animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;
    private int _animIDCrouch;
    private int _animIDRoll;

    private void Awake()
    {
        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        if (playerInput == null)
        {
            playerInput = FindObjectOfType<PlayerInput>();
        }
        if (playerInputReader == null)
        {
            playerInputReader = FindObjectOfType<StarterAssetsInputs>();
        }
    }
    private void Start()
    {
        playerMovementController.Initialize(playerInput, playerInputReader, characterController, _mainCamera);
        AssignAnimationIDs();
        SubscribeAnimationId();
        SetupConfiguration();
        SwitchState(new PlayerIdleState(this, StateID.Idle));
    }

    private void SetupConfiguration()
    {
        playerConfiguration.hitboxOriginalOffset = characterController.center;
        playerConfiguration.hitboxOriginalHeight = characterController.height;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnDestroy()
    {
        UnsubscribeAnimationId();
    }

    private void SubscribeAnimationId()
    {
        playerMovementController.SetAnimationBool += SetAnimation;
        playerMovementController.SetAnimationFloat += SetAnimation;
        playerMovementController.SetAnimationInt += SetAnimation;
        playerMovementController.SetAnimationString += SetAnimation;
        playerMovementController.SetAnimationTrigger += SetAnimation;
    }
    private void UnsubscribeAnimationId()
    {
        playerMovementController.SetAnimationBool -= SetAnimation;
        playerMovementController.SetAnimationFloat -= SetAnimation;
        playerMovementController.SetAnimationInt -= SetAnimation;
        playerMovementController.SetAnimationString -= SetAnimation;
        playerMovementController.SetAnimationTrigger -= SetAnimation;
    }
    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash(AnimationId.Speed.ToString());
        _animIDGrounded = Animator.StringToHash(AnimationId.Grounded.ToString());
        _animIDJump = Animator.StringToHash(AnimationId.Jump.ToString());
        _animIDFreeFall = Animator.StringToHash(AnimationId.FreeFall.ToString());
        _animIDMotionSpeed = Animator.StringToHash(AnimationId.MotionSpeed.ToString());
        _animIDCrouch = Animator.StringToHash(AnimationId.Crouch.ToString());
        _animIDRoll = Animator.StringToHash(AnimationId.Roll.ToString());
    }

    public void SetAnimation(AnimationId animationId, bool value)
    {
        // update animator if using character
        if (animator != null)
        {
            switch (animationId)
            {
                case AnimationId.Grounded:
                    animator.SetBool(_animIDGrounded, value);
                    break;
                case AnimationId.Jump:
                    animator.SetBool(_animIDJump, value);
                    if (currentStateID == StateID.Jump || value == false ) break;
                    SwitchState(new PlayerJumpState(this, currentStateID));
                    break;
                case AnimationId.FreeFall:
                    animator.SetBool(_animIDFreeFall, value);
                    if (currentStateID == StateID.Fall || value == false) break;
                    SwitchState(new PlayerFallState(this, currentStateID));
                    break;
                case AnimationId.Crouch:
                    animator.SetBool(_animIDCrouch, value);
                    if (currentStateID == StateID.Crouch && value == true) break;
                    if (currentStateID == StateID.Crouch && value == false)
                    {
                        SwitchState(new PlayerIdleState(this, currentStateID));
                        break;
                    }
                    else if (value == false) break;
                    SwitchState(new PlayerCrouchState(this, currentStateID));
                    break;
                case AnimationId.Roll:
                    animator.SetBool(_animIDRoll, value);
                    if ((currentStateID != StateID.Idle &&
                        currentStateID != StateID.Walk &&
                        currentStateID != StateID.Run &&
                        currentStateID != StateID.Crouch
                        ) || currentStateID == StateID.Roll || value == false)
                        break;
                    SwitchState(new PlayerRollState(this, currentStateID));
                    break;
            }
        }
    }
    public void SetAnimation(AnimationId animationId, float value)
    {
        // update animator if using character
        if (animator != null)
        {
            switch (animationId)
            {
                case AnimationId.Speed:
                    animator.SetFloat(_animIDSpeed, value);
                    if (animator.GetBool(_animIDJump)) break;
                    if (value == 0 && animator.GetBool(_animIDGrounded))
                    {
                        if (currentStateID == StateID.Idle) break;
                        SwitchState(new PlayerIdleState(this, currentStateID));
                        break;
                    }
                    else
                    {
                        if (animator.GetBool(_animIDGrounded))
                        {
                            if (playerInputReader.sprint)
                            {
                                if (currentStateID == StateID.Run) break;
                                SwitchState(new PlayerRunningState(this, currentStateID));
                                break;
                            }
                            else
                            {
                                if (currentStateID == StateID.Walk) break;
                                SwitchState(new PlayerWalkingState(this, currentStateID));
                                break;
                            }
                        }
                    }
                    break;
                case AnimationId.MotionSpeed:
                    animator.SetFloat(_animIDMotionSpeed, value);
                    break;
            }
        }
    }
    public void SetAnimation(AnimationId animationId, int value)
    {

    }
    public void SetAnimation(AnimationId animationId, string value)
    {

    }
    public void SetAnimation(AnimationId animationId)
    {
        
    }

}

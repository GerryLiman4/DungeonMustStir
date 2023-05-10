using StarterAssets;
using System;
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
    //[field: SerializeField] public GroundDetector groundDetector{ get; private set; }
    //[field: SerializeField] public AttackController attackController { get; private set; }

    private GameObject _mainCamera;

    // animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;


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
        //SwitchState(new PlayerIdleState(this, StateID.Idle));
    }
    protected override void Update()
    {
        base.Update();
        playerMovementController.JumpAndGravity();
        playerMovementController.GroundedCheck();
        playerMovementController.Move();
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    SwitchState(new PlayerWalkingState(this));
        //}
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
    }

    private void SetAnimation(AnimationId animationId, bool value)
    {
        // update animator if using character
        if (animator != null)
        {
            switch (animationId) {
                case AnimationId.Grounded:
                    animator.SetBool(_animIDGrounded, value);
                    break;
                case AnimationId.Jump:
                    animator.SetBool(_animIDJump, value);
                    break;
                case AnimationId.FreeFall:
                    animator.SetBool(_animIDFreeFall, value);
                    break;
            }
        }
    }
    private void SetAnimation(AnimationId animationId, float value)
    {
        // update animator if using character
        if (animator != null)
        {
            switch (animationId)
            {
                case AnimationId.Speed:
                    animator.SetFloat(_animIDSpeed, value);
                    break;
                case AnimationId.MotionSpeed:
                    animator.SetFloat(_animIDMotionSpeed, value);
                    break;
            }
        }
    }
    private void SetAnimation(AnimationId animationId, int value)
    {

    }
    private void SetAnimation(AnimationId animationId, string value)
    {

    }
    private void SetAnimation(AnimationId animationId)
    {

    }

}

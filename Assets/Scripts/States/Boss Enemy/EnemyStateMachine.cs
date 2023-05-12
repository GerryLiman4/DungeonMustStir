using StarterAssets;
using System;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    // Input Reader replacement for ai
    //[field: SerializeField] public StarterAssetsInputs playerInputReader { get; private set; }
    [field: SerializeField] public Animator animator { get; private set; }
    // Character Movement Controller replacement for player movement controller
    //[field: SerializeField] public PlayerMovementController playerMovementController { get; private set; }
    [field: SerializeField] public CharacterController characterController { get; private set; }
    // character configuration replacement for player configuration
    //[field: SerializeField] public PlayerConfiguration playerConfiguration { get; private set; }
    
    //private GameObject _mainCamera;

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
        //if (playerInputReader == null)
        //{
        //    playerInputReader = FindObjectOfType<StarterAssetsInputs>();
        //}
    }
    private void Start()
    {
        //playerMovementController.Initialize(playerInput, playerInputReader, characterController, _mainCamera);
        AssignAnimationIDs();
        SubscribeAnimationId();
        SetupConfiguration();
        SwitchState(new EnemyIdleState(this, StateID.Idle));
    }

    private void SetupConfiguration()
    {
        //playerConfiguration.hitboxOriginalOffset = characterController.center;
        //playerConfiguration.hitboxOriginalHeight = characterController.height;
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
        //playerMovementController.SetAnimationBool += SetAnimation;
        //playerMovementController.SetAnimationFloat += SetAnimation;
        //playerMovementController.SetAnimationInt += SetAnimation;
        //playerMovementController.SetAnimationString += SetAnimation;
        //playerMovementController.SetAnimationTrigger += SetAnimation;
    }
    private void UnsubscribeAnimationId()
    {
       //playerMovementController.SetAnimationBool -= SetAnimation;
       //playerMovementController.SetAnimationFloat -= SetAnimation;
       //playerMovementController.SetAnimationInt -= SetAnimation;
       //playerMovementController.SetAnimationString -= SetAnimation;
       //playerMovementController.SetAnimationTrigger -= SetAnimation;
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
        
    }
    public void SetAnimation(AnimationId animationId, float value)
    {
        // update animator if using character
        
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

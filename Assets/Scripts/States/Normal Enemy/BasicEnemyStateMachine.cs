using UnityEngine;

public class BasicEnemyStateMachine : MonoBehaviour
{
    [field: SerializeField] public AIBrain aiBrain { get; private set; }
    [field: SerializeField] public CharacterController characterController { get; private set; }
    [field: SerializeField] public BasicEnemyController enemyMovementController { get; private set; }

    [field: SerializeField] public float fps = 10;
    public float fpsCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        fpsCounter = fps;
    }

    // Update is called once per frame
    void Update()
    {
        fpsCounter--;
        if (fpsCounter > 0) return;
        fpsCounter = fps;
        BehaviourStateID currentBehaviour = aiBrain.GetCurrentBehaviourStateId();
        switch (currentBehaviour)
        {
            case BehaviourStateID.NOTHING:
                break;
            case BehaviourStateID.PATROL:
                break;
            case BehaviourStateID.CHASE:
                break;
            case BehaviourStateID.WANDER:
                break;
            case BehaviourStateID.AGGRO:
                break;

        }
    }
}

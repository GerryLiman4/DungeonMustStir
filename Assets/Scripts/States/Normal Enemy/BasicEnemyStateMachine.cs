using UnityEngine;

public class BasicEnemyStateMachine : MonoBehaviour
{
    [field: SerializeField] public AIBrain aiBrain { get; private set; }
    [field: SerializeField] public CharacterController characterController { get; private set; }
    [field: SerializeField] public BasicEnemyController enemyMovementController { get; private set; }

    [field: SerializeField] public float fps = 5;
    public float fpsCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        fpsCounter = fps;
        //aiBrain.BehaviourSwitched +=
    }

    // Update is called once per frame
    void Update()
    {
        //fpsCounter--;
        aiBrain.thinkTimer -= Time.deltaTime;
        //if (fpsCounter > 0) return;
        //fpsCounter = fps;
        BehaviourStateID currentBehaviour = aiBrain.GetCurrentBehaviourStateId();
        Debug.Log(currentBehaviour);
        switch (currentBehaviour)
        {
            case BehaviourStateID.NOTHING:
                aiBrain.CheckEnemy();
                break;
            case BehaviourStateID.PATROL:
                break;
            case BehaviourStateID.CHASE:

                break;
            case BehaviourStateID.WANDER:
                break;
            case BehaviourStateID.AGGRO:
                break;
            case BehaviourStateID.ALARMED:
                aiBrain.LookEnemy();
                break;

        }
    }

    private void OnDrawGizmos()
    {
        
    }

}

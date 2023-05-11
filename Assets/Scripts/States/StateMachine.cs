using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    State currentState;
    protected StateID currentStateID;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (currentState != null)
        {
            currentState.Tick(Time.deltaTime);
        }
    }

    protected virtual void FixedUpdate()
    {
        if(currentState != null){
            currentState.FixedTick();
        }

    }
    protected virtual void LateUpdate(){
         if(currentState != null){
            currentState.LateTick();
        }
    }


    public void SwitchState(State newState)
    {
        if (currentState != null)
        {
            if (currentState.CurrentStateID == newState.CurrentStateID) return;
            currentState.Exit();
        }

        currentState = newState;
        currentStateID = newState.CurrentStateID;

        Debug.Log("masuk" + newState.CurrentStateID);
        
        if (currentState != null)
        {
            currentState.Enter();
        }
    }

    public StateID GetCurrentStateID(){
        
        return currentStateID;
    }

}

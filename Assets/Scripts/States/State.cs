using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract StateID CurrentStateID{get;set;}
    public abstract StateID PreviousStateID { get; set; }
    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void FixedTick();
    public abstract void LateTick();
    public abstract void Exit();
    
}

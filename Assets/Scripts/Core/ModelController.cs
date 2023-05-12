using UnityEngine;

public abstract class ModelController : MonoBehaviour
{
    public abstract void Move();
    public abstract void JumpAndGravity();
    public abstract void GroundedCheck();

}

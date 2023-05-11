using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "Create Configuration/Player Model Configuration", order = 1)]
public class PlayerConfiguration : ScriptableObject
{
    public float hitboxOriginalHeight;
    public Vector3 hitboxOriginalOffset;

    public float hitboxCrouchHeight;
    public Vector3 hitboxCrouchOffset;

}

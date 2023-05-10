using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Configuration", menuName = "Create Attack Configuration/New Configuration", order = 1)]
public class AttackConfiguration : ScriptableObject
{
    public AttackVariationId attackId;
    public float animationDuration;
    public float cancelTreshold;
    public string animationName;

    public bool isCancellable;
    public float damage;
    public int attackType;

    public BoxCollider hitBox;
}

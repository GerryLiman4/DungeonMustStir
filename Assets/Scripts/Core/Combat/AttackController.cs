using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField]private List<AttackConfiguration> attacks = new List<AttackConfiguration>();

    public AttackConfiguration GetFirstAttack() {

        return attacks.Count != 0 ? attacks[0] : null;
    }

    public AttackConfiguration GetNextAttack(AttackVariationId currentAttackId) {

        for (int i = 0; i < attacks.Count; i++) {
            if(attacks[i].attackId == currentAttackId)
            {
                return attacks.Count <= i + 1 ? null : attacks[i + 1];
            }
        }
        return null;
    }
}

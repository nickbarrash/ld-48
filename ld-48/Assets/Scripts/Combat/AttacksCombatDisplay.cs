using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksCombatDisplay : MonoBehaviour
{
    public GameObject attackPrefab;
    public List<AttackDisplay> attacks = new List<AttackDisplay>();

    public void init(List<Attack> newAttacks, bool isEnemy) {
        foreach(var attack in attacks) {
            Destroy(attack.gameObject);
        }
        attacks.Clear();

        foreach(var attack in newAttacks) {
            addAttack(attack, isEnemy);
        }
    }

    public void addAttack(Attack attack, bool isEnemy) {
        var newAttack = Instantiate(attackPrefab, transform);
        newAttack.transform.localPosition -= new Vector3(0, 50, 0) * (attacks.Count + 1);
        newAttack.name = attack.name;
        var newAttackDisplay = newAttack.GetComponent<AttackDisplay>();
        attacks.Add(newAttackDisplay);
        newAttackDisplay.setAttack(attack, isEnemy);
    }
}

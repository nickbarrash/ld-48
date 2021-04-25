using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        newAttack.name = attack.attackName;
        var newAttackDisplay = newAttack.GetComponent<AttackDisplay>();
        attacks.Add(newAttackDisplay);
        newAttackDisplay.setAttack(attack, isEnemy);
    }

    public void setTurn(bool isMyTurn) {
        foreach(var attack in attacks) {
            attack.setTurn(isMyTurn);
        }
    }

    public void cooldown() {
        foreach(var a in attacks) {
            a.decrementCooldown();
        }
    }

    public Attack getRandomAttack() {
        var attackList = attacks.Where(a => !a.isOnCooldown()).ToList();
        if (attackList.Count <= 0) {
            return null;
        }
        return attackList[Random.Range(0, attackList.Count - 1)].attack;
    }
}

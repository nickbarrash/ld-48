using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChallengerDisplay : MonoBehaviour
{
    public bool isEnemy;
    Enemy enemy;

    public int currentEnemyHealth;
    public float attackMultiplier;

    public AttacksCombatDisplay attacks;

    public TMP_Text challengerName;
    public TMP_Text attackMultipleValue;
    public HealthDisplay health;

    List<StatusEffect> activeEffects = new List<StatusEffect>();

    public void attackEnemy(Attack attack, float multiplier) {
        currentEnemyHealth -= (int)(attack.enemyDamage * multiplier);
        health.updateHealth(enemy.health, currentEnemyHealth);

        if (currentEnemyHealth <= 0) {
            FindObjectOfType<CombatDisplay>().playerVictory();
        }
    }

    public void attackPlayer(Attack attack, float multiplier) {
        GameManager.instance.player.updateHealth(-1 * (int)(attack.enemyDamage * multiplier));
        health.updateHealth(GameManager.instance.player.maxHealth, GameManager.instance.player.health);
    }

    public void applyStatuses(Attack attack, bool isSelf) {
        //if (attack.effects != null && attack.effects.Count > 0) {
        //    activeEffects.Add(new StatusEffect {
        //        activeTurns = attack.effects[0].activeTurns,
        //        selfAttackMultiple = attack.effects[0].enemyAttackMultiple
        //    });
        //}
    }

    public void cooldown() {
        attacks.cooldown();
        // TODO: status effects
    }

    public void initEnemy(Enemy enemy) {
        isEnemy = true;
        this.enemy = enemy;
        attacks.init(enemy.attacks, true);
        activeEffects = new List<StatusEffect>();

        currentEnemyHealth = enemy.health;
        health.updateHealth(enemy.health, currentEnemyHealth);
        challengerName.text = enemy.enemyName;
        computeMultiplier();
    }

    public void initPlayer() {
        isEnemy = false;
        activeEffects = new List<StatusEffect>();
        attacks.init(GameManager.instance.player.attacks, false);

        health.updateHealth(GameManager.instance.player.maxHealth, GameManager.instance.player.health);
        challengerName.text = "Your Drilling Rig";
        computeMultiplier();
    }

    public void computeMultiplier() {
        attackMultiplier = 1f;
        foreach (var effect in activeEffects) {
            attackMultiplier *= effect.selfAttackMultiple;
        }

        attackMultipleValue.text = attackMultiplier.ToString();
    }

    public void setTurn(bool isMyTurn) {
        attacks.setTurn(isMyTurn);
    }
}

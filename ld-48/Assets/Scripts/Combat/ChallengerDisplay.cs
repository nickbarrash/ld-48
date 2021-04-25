using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChallengerDisplay : MonoBehaviour
{
    public bool isEnemy;
    Enemy enemy;

    int currentEnemyHealth;
    float attackMultiplier;

    public AttacksCombatDisplay attacks;

    public TMP_Text challengerName;
    public TMP_Text attackMultipleValue;
    public HealthDisplay health;

    List<StatusEffect> activeEffects = new List<StatusEffect>();

    public void initEnemy(Enemy enemy) {
        isEnemy = true;
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
}

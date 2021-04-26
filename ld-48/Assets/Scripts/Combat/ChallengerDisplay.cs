using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class EnemyIcon {
    public TerrainSquare.TERRAIN_TYPE type;
    public Image icon;
    public string match;
}


public class ChallengerDisplay : MonoBehaviour
{
    public bool isEnemy;
    public Enemy enemy;

    public int currentEnemyHealth;
    public float attackMultiplier;

    public AttacksCombatDisplay attacks;

    public TMP_Text challengerName;
    public TMP_Text attackMultipleValue;
    public HealthDisplay health;

    List<StatusEffect> activeEffects = new List<StatusEffect>();

    public List<EnemyIcon> icons;
    public GameObject icon;

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
        if (attack.effects != null && attack.effects.Count > 0) {
            activeEffects.Add(new StatusEffect {
                activeTurns = attack.effects[0].activeTurns,
                selfAttackMultiple = isSelf ? attack.effects[0].selfAttackMultiple : attack.effects[0].enemyAttackMultiple
            });
            computeMultiplier();
        }
    }

    public void cooldown() {
        attacks.cooldown();
        cooldownStatusEffects();
    }

    private void cooldownStatusEffects() {
        foreach(var effect in activeEffects) {
            effect.activeTurns--;
        }

        activeEffects = activeEffects.Where(e => e.activeTurns > 0).ToList();

        computeMultiplier();
    }

    public void hideIcons() {
        foreach(var i in icons) {
            if (i.icon != null) {
                i.icon.gameObject.SetActive(false);
            }
        }
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

        hideIcons();
        var icon = icons.FirstOrDefault(i => i.match == enemy.enemyName);
        if (icon != null && icon.icon != null) {
            icon.icon.gameObject.SetActive(true);
        }
    }

    public void initPlayer() {
        isEnemy = false;
        activeEffects = new List<StatusEffect>();
        attacks.init(GameManager.instance.player.attacks, false);

        health.updateHealth(GameManager.instance.player.maxHealth, GameManager.instance.player.health);
        challengerName.text = "Your Drilling Rig";
        computeMultiplier();
        hideIcons();
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackDisplay : MonoBehaviour
{
    public Attack attack;

    public Button attackButton;

    public TMP_Text attackNameLabel;
    public TMP_Text descriptionLabel;
    public TMP_Text damageValue;
    public TMP_Text cooldownValue;
    public TMP_Text cooldownRemainingValue;

    public GameObject onCooldownPanel;
    public GameObject readyPanel;

    CombatDisplay combat;
    bool isEnemy;

    int cooldown = 0;

    public void setAttack(Attack attack, bool isEnemy) {
        this.attack = attack;

        attackNameLabel.text = attack.attackName;
        descriptionLabel.text = attack.description;
        damageValue.text = attack.enemyDamage.ToString();
        cooldownValue.text = attack.cooldown.ToString();
        cooldownRemainingValue.text = 0.ToString();
        onCooldownPanel.SetActive(false);
        readyPanel.SetActive(true);

        attackButton.enabled = !isEnemy;

        combat = FindObjectOfType<CombatDisplay>();
        this.isEnemy = isEnemy;
    }

    public void setTurn(bool isMyTurn) {
        attackButton.interactable = isMyTurn && !isOnCooldown();
    }

    public void decrementCooldown() {
        if (cooldown > 0) {
            cooldown--;
        }
        cooldownAffordance();
    }

    public bool isOnCooldown() {
        return cooldown > 0;
    }

    public void cooldownAffordance() {
        if (cooldown <= 0) {
            onCooldownPanel.SetActive(false);
            readyPanel.SetActive(true);
            return;
        }

        cooldownRemainingValue.text = cooldown.ToString();

        onCooldownPanel.SetActive(true);
        readyPanel.SetActive(false);
    }

    public void doAttack() {
        combat.attack(attack, isEnemy);
        cooldown = attack.cooldown;
        cooldownAffordance();
    }
}

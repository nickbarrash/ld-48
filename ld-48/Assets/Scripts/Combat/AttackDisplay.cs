using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackDisplay : MonoBehaviour
{
    Attack attack;

    public Button attackButton;

    public TMP_Text attackNameLabel;
    public TMP_Text descriptionLabel;
    public TMP_Text damageValue;
    public TMP_Text cooldownValue;
    public TMP_Text cooldownRemainingValue;

    public GameObject onCooldownPanel;
    public GameObject readyPanel;
    

    public void setAttack(Attack attack, bool isEnemy) {
        this.attack = attack;

        attackNameLabel.text = attack.name;
        descriptionLabel.text = attack.description;
        damageValue.text = attack.enemyDamage.ToString();
        cooldownValue.text = attack.cooldown.ToString();
        cooldownRemainingValue.text = 0.ToString();
        onCooldownPanel.SetActive(false);
        readyPanel.SetActive(true);

        attackButton.enabled = !isEnemy;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatDisplay : MonoBehaviour
{
    float ENEMY_ATTACK_DELAY = 0.5f;

    public ChallengerDisplay playerDisplay;
    public ChallengerDisplay enemyDisplay;

    bool isPlayersTurn;

    public void startCombat(Enemy enemy) {
        gameObject.SetActive(true);
        enemyDisplay.initEnemy(enemy);
        playerDisplay.initPlayer();

        isPlayersTurn = true;
        setTurn();
    }

    public void setTurn() {
        playerDisplay.setTurn(isPlayersTurn);
        enemyDisplay.setTurn(false);
    }

    public void attack(Attack attack, bool isHitPlayer) {
        if (!isHitPlayer) {
            if (attack != null) {
                playerDisplay.applyStatuses(attack, true);
                enemyDisplay.applyStatuses(attack, false);
                enemyDisplay.attackEnemy(attack, playerDisplay.attackMultiplier);
            }
        } else {
            if (attack != null) {
                playerDisplay.applyStatuses(attack, false);
                enemyDisplay.applyStatuses(attack, true);
                playerDisplay.attackPlayer(attack, enemyDisplay.attackMultiplier);
            }
        }

        endAttack();
    }

    public void endAttack() {
        if (isPlayersTurn) {
            playerDisplay.cooldown();
        } else {
            enemyDisplay.cooldown();
        }

        isPlayersTurn = !isPlayersTurn;
        setTurn();

        if (!isPlayersTurn && enemyDisplay.currentEnemyHealth > 0) {
            enemyAttack();
        }
    }

    public void enemyAttack() {
        StartCoroutine(delayedEnemyAttack());
    }

    private IEnumerator delayedEnemyAttack() {
        yield return new WaitForSeconds(ENEMY_ATTACK_DELAY);
        AttackDisplay enemyAttack = enemyDisplay.attacks.getRandomAttack();
        if (enemyDisplay.attacks.getRandomAttack() != null) {
            enemyAttack.doAttack();
        } else {
            endAttack();
        }
    }

    public void playerVictory() {
        gameObject.SetActive(false);
        GameManager.instance.player.attacks.First(a => a.attackName == "Drill").enemyDamage += 1;
        ToastManager.instance.showMessage($"Successfully killed {enemyDisplay.enemy.enemyName}. Your drill's damage has increased by 1");
        if (enemyDisplay.enemy.enemyName == "Balrog") {
            GameManager.instance.gameOver(true);
        }
    }
}

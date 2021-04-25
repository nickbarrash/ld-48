using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDisplay : MonoBehaviour
{
    public ChallengerDisplay playerDisplay;
    public ChallengerDisplay enemyDisplay;

    public void startCombat(Enemy enemy) {
        gameObject.SetActive(true);
        enemyDisplay.initEnemy(enemy);
        playerDisplay.initPlayer();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreDisplay : MonoBehaviour
{
    int BOMB_PRICE = 1000;
    int SCAN_PRICE = 200;
    int REPAIR_PRICE = 25;
    int HULL_UPGRADE_PRICE = 150;
    int EARTHQUAKE_PRICE = 16000;
    int LIQUIFACTION_PRICE = 5000;

    bool purchasedEarthquake = false;
    bool purchasedLiquifaction = false;

    private void Start() {
        setVisible(false);
    }

    public void setVisible(bool isVisible) {
        gameObject.SetActive(isVisible);
    }

    public void buyRepair() {
        if (GameManager.instance.player.money > REPAIR_PRICE && GameManager.instance.player.health < GameManager.instance.player.maxHealth) {
            GameManager.instance.player.updateHealth(20);
            GameManager.instance.player.updateMoney(-1 * REPAIR_PRICE);
        }
    }

    public void buyHullUpgrade() {
        if (GameManager.instance.player.money > HULL_UPGRADE_PRICE) {
            GameManager.instance.player.updateHealth(20);
            GameManager.instance.player.maxHealth += 20;
            GameManager.instance.player.updateMoney(-1 * HULL_UPGRADE_PRICE);
        }
    }
}

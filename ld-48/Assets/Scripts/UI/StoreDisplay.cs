using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreDisplay : MonoBehaviour
{
    int BOMB_PRICE = 500;
    int SCAN_PRICE = 200;
    int REPAIR_PRICE = 25;
    int HULL_UPGRADE_PRICE = 150;
    int EARTHQUAKE_PRICE = 10000;
    int LIQUEFACTION_PRICE = 4000;

    bool purchasedEarthquake = false;
    bool purchasedLiquefaction = false;

    public GameObject earthquakeItem;
    public GameObject liquefactionItem;

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
            GameManager.instance.player.maxHealth += 20;
            GameManager.instance.player.updateHealth(20);
            GameManager.instance.player.updateMoney(-1 * HULL_UPGRADE_PRICE);
        }
    }

    public void buyBomb() {
        if (GameManager.instance.player.money > BOMB_PRICE) {
            GameManager.instance.player.updateBombs(1);
            GameManager.instance.player.updateMoney(-1 * BOMB_PRICE);
        }
    }

    public void buyScan() {
        if (GameManager.instance.player.money > SCAN_PRICE) {
            GameManager.instance.player.updateScans(1);
            GameManager.instance.player.updateMoney(-1 * SCAN_PRICE);
        }
    }

    public void buyEarthquake() {
        if (GameManager.instance.player.money > EARTHQUAKE_PRICE && !purchasedEarthquake) {
            purchasedEarthquake = true;
            earthquakeItem.SetActive(false);
            GameManager.instance.player.addAttack(Attack.earthquake());
            GameManager.instance.player.updateMoney(-1 * EARTHQUAKE_PRICE);
        }
    }

    public void buyLiquefaction() {
        if (GameManager.instance.player.money > LIQUEFACTION_PRICE && !purchasedLiquefaction) {
            purchasedLiquefaction = true;
            liquefactionItem.SetActive(false);
            GameManager.instance.player.addAttack(Attack.liquefaction());
            GameManager.instance.player.updateMoney(-1 * LIQUEFACTION_PRICE);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreDisplay : MonoBehaviour
{
    int BOMB_PRICE = 1000;
    int SCAN_PRICE = 200;
    int REPAIR_PRICE = 10;
    int GAS_PRICE = 10;
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

    public void buyGas() {
        if (GameManager.instance.player.money > GAS_PRICE) {
            GameManager.instance.player.updateGas(20);
            GameManager.instance.player.updateMoney(-1 * GAS_PRICE);
        }
    }
}

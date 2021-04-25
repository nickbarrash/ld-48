using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const int INITAL_MAX_HEALTH = 100;
    const int INITIAL_MONEY = 100;
    const int INITAL_MAX_GAS = 100;
    const int INITIAL_GAS = 100;

    const int DEPTH_MULTIPLE = 25;

    [HideInInspector]
    public int maxHealth;
    [HideInInspector]
    public int health;
    [HideInInspector]
    public int money;
    [HideInInspector]
    public int depth;
    [HideInInspector]
    public int maxGas;
    [HideInInspector]
    public int gas;

    public List<Attack> attacks = new List<Attack> {
        Attack.drill()
    };

    HealthDisplay healthDisplay;
    GasDisplay gasDisplay;
    MoneyDisplay moneyDisplay;
    DepthDisplay depthDisplay;

    private void Awake() {
        maxHealth = INITAL_MAX_HEALTH;
        health = INITAL_MAX_HEALTH;
        money = INITIAL_MONEY;
        maxGas = INITAL_MAX_GAS;
        gas = INITIAL_GAS;

        healthDisplay = GameObject.Find("HUD/PlayerAttributesSection/HealthSection").GetComponent<HealthDisplay>();
        gasDisplay = FindObjectOfType<GasDisplay>();
        moneyDisplay = FindObjectOfType<MoneyDisplay>();
        depthDisplay = FindObjectOfType<DepthDisplay>();
    }

    private void Start() {
        updateMoney(0);
        updateHealth(0);
        updateGas(0);
    }

    public void processMineAction(TerrainSquare.TERRAIN_TYPE minedType) {
        var action = Config.instance.getMineAction(minedType);
        updateHealth(action.damage * -1);
        updateMoney(action.money);
    }

    public void setDepth(int y) {
        depth = Mathf.Max(depth, DEPTH_MULTIPLE * y);
        depthDisplay.updateDepth();
    }

    public void processDepth(int y) {
        setDepth(y);
        updateGas(-1 * TerrainGrid.gasConsumed(y));
    }

    private void updateHealth(int diff) {
        health += diff;
        if (health <= 0) {
            GameManager.instance.gameOver();
        }

        healthDisplay.updateHealth(maxHealth, health);
    }

    private void updateGas(int diff) {
        gas += diff;
        if (gas <= 0) {
            GameManager.instance.gameOver();
        }

        gasDisplay.updateGas();
    }

    private void updateMoney(int diff) {
        money += diff;

        moneyDisplay.updateMoney();
    }
}

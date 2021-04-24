using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const int INITAL_MAX_HEALTH = 100;
    const int INITIAL_MONEY = 100;

    const int DEPTH_MULTIPLE = 25;

    [HideInInspector]
    public int maxHealth;
    [HideInInspector]
    public int health;
    [HideInInspector]
    public int money;
    public int depth;

    HealthDisplay healthDisplay;
    MoneyDisplay moneyDisplay;
    DepthDisplay depthDisplay;

    private void Awake() {
        maxHealth = INITAL_MAX_HEALTH;
        health = INITAL_MAX_HEALTH;
        money = INITIAL_MONEY;

        healthDisplay = FindObjectOfType<HealthDisplay>();
        moneyDisplay = FindObjectOfType<MoneyDisplay>();
        depthDisplay = FindObjectOfType<DepthDisplay>();
    }

    private void Start() {
        updateMoney(0);
        updateHealth(0);
    }

    public void processMineAction(TerrainSquare.TERRAIN_TYPE minedType) {
        Debug.Log($"mined: {minedType}");
        var action = Config.instance.getMineAction(minedType);
        updateHealth(action.damage * -1);
        updateMoney(action.money);
    }

    public void updateDepth(int y) {
        depth = Mathf.Max(depth, DEPTH_MULTIPLE * y);
        depthDisplay.updateDepth();
    }

    private void updateHealth(int diff) {
        health += diff;
        if (health <= 0) {
            GameManager.instance.gameOver();
        }

        healthDisplay.updateHealth();
    }

    private void updateMoney(int diff) {
        money += diff;

        moneyDisplay.updateMoney();
    }
}

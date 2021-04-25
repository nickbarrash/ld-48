using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    const int INITAL_MAX_HEALTH = 100;
    const int INITIAL_MONEY = 100;
    const int INITIAL_BOMBS = 1;
    const int INITIAL_SCANS = 2;

    const int DEPTH_MULTIPLE = 25;

    [HideInInspector]
    public int maxHealth;
    [HideInInspector]
    public int health;
    [HideInInspector]
    public int money;
    [HideInInspector]
    public int depth;

    public List<Attack> attacks = new List<Attack> {
        Attack.drill()
    };

    HealthDisplay healthDisplay;
    GasDisplay gasDisplay;
    MoneyDisplay moneyDisplay;
    DepthDisplay depthDisplay;

    public Button scanButton;
    public Button bombButton;

    public TMP_Text scanButtonLabel;
    public TMP_Text bombButtonLabel;

    public ExcavateTypeUI excavateButtonManager;

    public int bombCount;
    public int scanCount;

    private void Awake() {
        maxHealth = INITAL_MAX_HEALTH;
        health = INITAL_MAX_HEALTH;
        money = INITIAL_MONEY;
        bombCount = INITIAL_BOMBS;
        scanCount = INITIAL_SCANS;

        healthDisplay = GameObject.Find("HUD/PlayerAttributesSection/HealthSection").GetComponent<HealthDisplay>();
        gasDisplay = FindObjectOfType<GasDisplay>();
        moneyDisplay = FindObjectOfType<MoneyDisplay>();
        depthDisplay = FindObjectOfType<DepthDisplay>();
    }

    private void Start() {
        updateMoney(0);
        updateHealth(0);
        updateScans(0);
        updateBombs(0);

        excavateButtonManager.setSelectedExcavation();
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
    }

    public void updateScans(int diff) {
        scanCount += diff;
        scanButtonLabel.text = $"Scan ({scanCount})";
        scanButton.interactable = scanCount > 0;

        if (scanCount <= 0 && GameManager.instance.actionType == GameManager.CLICK_ACTION_TYPE.SCAN) {
            GameManager.instance.actionType = GameManager.CLICK_ACTION_TYPE.EXCAVATE;
        }
        excavateButtonManager.setSelectedExcavation();
    }

    public void updateBombs(int diff) {
        bombCount += diff;
        bombButtonLabel.text = $"Bomb ({bombCount})";
        bombButton.interactable = bombCount > 0;

        if (bombCount <= 0 && GameManager.instance.actionType == GameManager.CLICK_ACTION_TYPE.BOMB) {
            GameManager.instance.actionType = GameManager.CLICK_ACTION_TYPE.EXCAVATE;
        }
        excavateButtonManager.setSelectedExcavation();
    }

    public void updateHealth(int diff) {
        health += diff;
        if (health <= 0) {
            GameManager.instance.gameOver();
        }

        healthDisplay.updateHealth(maxHealth, health);
    }

    public void updateMoney(int diff) {
        money += diff;

        moneyDisplay.updateMoney();
    }
}

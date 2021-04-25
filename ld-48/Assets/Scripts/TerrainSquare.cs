using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TerrainSquare : MonoBehaviour
{
    public enum STATE {
        EXCAVATED,
        UNEXCAVATED
    }

    [Serializable]
    public enum TERRAIN_TYPE {
        EMPTY,

        DENSE_ROCK,
        GAS_POCKET,

        COPPER,
        SILVER,
        GOLD,
        PLATINUM,
        DIAMOND,

        SPIDER,
        ROCKMAN,
        WORM,
        BALROG
    }

    public HashSet<TERRAIN_TYPE> COMBAT = new HashSet<TERRAIN_TYPE> {
        TERRAIN_TYPE.SPIDER,
        TERRAIN_TYPE.ROCKMAN,
        TERRAIN_TYPE.WORM,
        TERRAIN_TYPE.BALROG
    };

    public HashSet<TERRAIN_TYPE> DANGEROUS = new HashSet<TERRAIN_TYPE> {
        TERRAIN_TYPE.DENSE_ROCK,
        TERRAIN_TYPE.GAS_POCKET,

        TERRAIN_TYPE.SPIDER,
        TERRAIN_TYPE.ROCKMAN,
        TERRAIN_TYPE.WORM,
        TERRAIN_TYPE.BALROG
    };

    public HashSet<TERRAIN_TYPE> LOOT = new HashSet<TERRAIN_TYPE> {
        TERRAIN_TYPE.COPPER,
        TERRAIN_TYPE.SILVER,
        TERRAIN_TYPE.GOLD,
        TERRAIN_TYPE.PLATINUM,
        TERRAIN_TYPE.DIAMOND
    };

    public TerrainGrid grid;
    public int x;
    public int y;
    public TERRAIN_TYPE type;
    public STATE state;
    //public TerrainInfoTiles infoTiles;
    public DisplayValueManager displayValue;

    public TMP_Text debugText;
    public TMP_Text contentsLabel;
    public GameObject excavated;
    public GameObject unexcavated;
    public GameObject contents;
    public Button selectButton;

    public Color COLOR_OSCILATE_1;
    public Color COLOR_OSCILATE_2;
    public float OSCILATION_RATE;

    [HideInInspector]
    public bool isScanned = false;

    ColorBlock tmpButtonColorBlock;

    public IEnumerable<TerrainSquare> accessibleNeighbors() {
        if (grid.getSquare(x + 1, y) != null) {
            yield return grid.getSquare(x + 1, y);
        }

        if (grid.getSquare(x - 1, y) != null) {
            yield return grid.getSquare(x - 1, y);
        }

        if (grid.getSquare(x, y - 1) != null) {
            yield return grid.getSquare(x, y - 1);
        }

        if (grid.getSquare(x, y + 1) != null) {
            yield return grid.getSquare(x, y + 1);
        }
    }

    public IEnumerable<TerrainSquare> neighbors() {
        for (int xOff = -1; xOff <= 1; xOff++) {
            for (int yOff = -1; yOff <= 1; yOff++) {
                if (xOff != 0 || yOff != 0) {
                    var neighbor = grid.getSquare(x + xOff, y + yOff);
                    if (neighbor != null) {
                        yield return neighbor;
                    }
                }
            }
        }
    }

    public IEnumerable<TerrainSquare> unexcavatedNeighbors() {
        return neighbors().Where(n => n.state != STATE.EXCAVATED);
    }

    public void setDisplayValues() {
        if (!isValueDisplayable()) {
            displayValue.hide();
            return;
        }

        displayValue.setValues(
            unexcavatedNeighbors().Where(t => DANGEROUS.Contains(t.type)).Count(),
            unexcavatedNeighbors().Where(t => LOOT.Contains(t.type)).Count(),
            unexcavatedNeighbors().Where(t => COMBAT.Contains(t.type)).Count()
        );

        displayValue.showValue(GameManager.instance.displayType);
    }

    public void setAffordances() {
        setDisplayValues();
        setExcavatedAffordances(state == STATE.EXCAVATED);
        setButtonEnabled();
        setDebug();
        setContents();
    }

    private void setDebug() {
        if (GameManager.instance.DEBUG) {
            debugText.text = $"{type} ({x},{y})";
        } else {
            debugText.text = "";
        }
    }

    private void setContents() {
        if (isScanned && isScanable()) {
            contents.SetActive(true);
            contentsLabel.text = $"{type}";
        } else {
            contents.SetActive(false);
        }
    }

    private void setExcavatedAffordances(bool isExcavated) {
        excavated.SetActive(isExcavated);
        unexcavated.SetActive(!isExcavated);
    }

    private bool isValueDisplayable() {
        return state == STATE.EXCAVATED && neighbors().FirstOrDefault(n => n.state == STATE.UNEXCAVATED) != null;
    }

    private bool isButtonEnabled() {
        return state == STATE.UNEXCAVATED && accessibleNeighbors().FirstOrDefault(n => n.state == STATE.EXCAVATED) != null;
    }

    private bool isScanable() {
        return state == STATE.UNEXCAVATED;
    }

    private bool isBombable() {
        return state == STATE.UNEXCAVATED;
    }

    public void setButtonEnabled() {
        selectButton.gameObject.SetActive(isButtonEnabled());
    }

    public void setState(STATE newState) {
        state = newState;
        setAffordances();
    }

    public void spawn(TERRAIN_TYPE newType, int x, int y, bool isInitial) {
        this.x = x;
        this.y = y;

        if (isInitial) {
            type = TERRAIN_TYPE.EMPTY;
            state = STATE.EXCAVATED;
            GameManager.instance.player.setDepth(this.y);
            return;
        }

        type = newType;
        state = STATE.UNEXCAVATED;
    }

    public void excavate() {
        processType();
        setState(STATE.EXCAVATED);
        updateNeighborAffordances();
    }

    public void updateNeighborAffordances() {
        foreach (var n in neighbors()) {
            n.setAffordances();
        }
    }

    public void scan() {
        if (!isScanned && isScanable() && GameManager.instance.player.scanCount > 0) {
            isScanned = true;
            setAffordances();
            ToastManager.instance.showMessage($"Scanned {Config.instance.getTerrainName(type)}");
            GameManager.instance.player.updateScans(-1);
        }
    }

    public void bomb() {
        if (isBombable() && GameManager.instance.player.bombCount > 0) {
            GameManager.instance.player.processDepth(y);
            GameManager.instance.player.updateBombs(-1);
            setState(STATE.EXCAVATED);
            ToastManager.instance.showMessage($"Destroyed {Config.instance.getTerrainName(type)}");
            updateNeighborAffordances();
        }
    }

    public void act() {
        if (GameManager.instance.actionType == GameManager.CLICK_ACTION_TYPE.EXCAVATE) {
            excavate();
        } else if (GameManager.instance.actionType == GameManager.CLICK_ACTION_TYPE.SCAN) {
            scan();
        } else if (GameManager.instance.actionType == GameManager.CLICK_ACTION_TYPE.BOMB) {
            bomb();
        }
    }

    public void processType() {
        GameManager.instance.player.processMineAction(type);
        GameManager.instance.player.processDepth(y);

        var enemy = Config.instance.getMineAction(type).enemy;
        if (enemy.Count > 0) {
            GameManager.instance.startCombat(enemy[0]);
        }
    }

    private void Update() {
        // TODO: move oscilation
        if (selectButton.gameObject.activeSelf) {
            tmpButtonColorBlock = selectButton.colors;
            tmpButtonColorBlock.normalColor = Color.Lerp(COLOR_OSCILATE_2, COLOR_OSCILATE_1, (Mathf.Sin(Time.time * OSCILATION_RATE) + 1) / 2);
            selectButton.colors = tmpButtonColorBlock;
        }
    }
}

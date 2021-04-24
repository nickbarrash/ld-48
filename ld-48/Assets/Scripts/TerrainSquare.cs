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
        DIAMOND
    }

    public TerrainGrid grid;
    public int x;
    public int y;
    int displayValue;
    public TERRAIN_TYPE type;
    public STATE state;
    public TerrainInfoTiles infoTiles;

    public TMP_Text debugText;
    public GameObject excavated;
    public GameObject unexcavated;
    public Button selectButton;

    public Color COLOR_OSCILATE_1;
    public Color COLOR_OSCILATE_2;
    public float OSCILATION_RATE;

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

    public void setTerrainInfo() {
        if (!isValueDisplayable()) {
            infoTiles.showTileInfo(false);
            return;
        }

        infoTiles.showTileInfo(true);

        var types = new Dictionary<TERRAIN_TYPE, int>();
        foreach (var n in unexcavatedNeighbors()) {
            if (!types.ContainsKey(n.type)) {
                types[n.type] = 0;
            }
            types[n.type]++;
        }
        infoTiles.setTerrainInfo(types.AsEnumerable());
    }

    public void setAffordances() {
        setTerrainInfo();
        setExcavatedAffordances(state == STATE.EXCAVATED);
        setButtonEnabled();
        setDebug();
    }

    private void setDebug() {
        if (GameManager.instance.DEBUG) {
            debugText.text = $"{type}";
        } else {
            debugText.text = "";
        }
    }

    private void setExcavatedAffordances(bool isExcavated) {
        excavated.SetActive(isExcavated);
        unexcavated.SetActive(!isExcavated);
    }

    private bool isValueDisplayable() {
        return state == STATE.EXCAVATED && accessibleNeighbors().FirstOrDefault(n => n.state == STATE.UNEXCAVATED) != null;
    }

    private bool isButtonEnabled() {
        return state == STATE.UNEXCAVATED && accessibleNeighbors().FirstOrDefault(n => n.state == STATE.EXCAVATED) != null;
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
            return;
        }

        type = newType;
        state = STATE.UNEXCAVATED;
    }

    public void excavate() {
        processType();
        setState(STATE.EXCAVATED);
        foreach(var n in neighbors()) {
            n.setAffordances();
        }
    }

    public void processType() {
        Debug.Log($"Excavated {type}");
    }

    private void Update() {
        if (selectButton.gameObject.activeSelf) {
            tmpButtonColorBlock = selectButton.colors;
            tmpButtonColorBlock.normalColor = Color.Lerp(COLOR_OSCILATE_2, COLOR_OSCILATE_1, Mathf.Sin(Time.time * OSCILATION_RATE));
            selectButton.colors = tmpButtonColorBlock;
        }
    }
}

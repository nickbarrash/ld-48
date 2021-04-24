using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TerrainSquare : MonoBehaviour
{
    public enum STATE {
        EXCAVATED,
        UNEXCAVATED
    }

    public enum TERRAIN_TYPE {
        EMPTY = 0,
        DENSE_ROCK = -7,
        GAS_POCKET = -13,
        COPPER = 2,
        SILVER = 5,
        GOLD = 10,
        PLATINUM = 20,
        DIAMOND = 100
    }

    public TerrainGrid grid;
    public int x;
    public int y;
    int displayValue;
    public TERRAIN_TYPE type;
    public STATE state;

    public TMP_Text valueText;
    public GameObject excavated;
    public GameObject unexcavated;
    public GameObject selectButton;


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

    public int getValue() {
        return state == STATE.EXCAVATED ? 0 : (int)type;
    }

    public int getDisplayValue() {
        var displayVal = 0;
        foreach (var n in accessibleNeighbors()) {
            displayVal += n.getValue();
        }
        return displayVal;
    }

    public void setAffordances() {
        setValueText();
        setExcavatedAffordances(state == STATE.EXCAVATED);
        setButtonEnabled();
    }

    private void setValueText() {
        if (grid.debugMode) {
            valueText.text = $"{getValue()}";
        } else {
            if (isValueDisplayable()) {
                valueText.text = $"{getDisplayValue()}";
            } else {
                valueText.text = $"";
            }
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

    public void setButtonEnabled() {
        selectButton.SetActive(isButtonEnabled());
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
            setState(STATE.EXCAVATED);
            return;
        }

        type = newType;
        setState(STATE.UNEXCAVATED);
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
}

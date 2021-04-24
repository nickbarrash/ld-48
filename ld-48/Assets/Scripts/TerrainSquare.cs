using System.Collections;
using System.Collections.Generic;
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

    public void setAffordances() {
        if (grid.debugMode) {
            valueText.text = $"{getValue()}";
        } else {
            valueText.text = $"NYI";
        }

        setExcavatedAffordances(state == STATE.EXCAVATED);
    }

    private void setExcavatedAffordances(bool isExcavated) {
        excavated.SetActive(isExcavated);
        unexcavated.SetActive(!isExcavated);
    }

    public void setState(STATE newState) {
        state = newState;
        setAffordances();
    }

    public void spawn(TERRAIN_TYPE newType, int x, int y) {
        this.x = x;
        this.y = y;
        type = newType;
        setState(STATE.UNEXCAVATED);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TypeColor {
    public TerrainSquare.TERRAIN_TYPE type;
    public Color color;
}

[Serializable]
public class MineAction {
    public TerrainSquare.TERRAIN_TYPE type;
    public int damage;
    public int money;
}

public class Config : MonoBehaviour
{
    public List<TypeColor> COLOR_MAP;
    public List<MineAction> MINE_ACTION_MAP;

    public Color DANGER_LOW;
    public Color DANGER_HIGH;

    public Color LOOT_LOW;
    public Color LOOT_HIGH;

    Dictionary<TerrainSquare.TERRAIN_TYPE, Color> terrainColorMap = new Dictionary<TerrainSquare.TERRAIN_TYPE, Color>();
    Dictionary<TerrainSquare.TERRAIN_TYPE, MineAction> terrainActionMap = new Dictionary<TerrainSquare.TERRAIN_TYPE, MineAction>();

    public static Config instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogWarning("Destroying dup config");
            Destroy(this);
        }

        foreach(var tc in COLOR_MAP) {
            terrainColorMap[tc.type] = tc.color;
        }

        foreach(var tm in MINE_ACTION_MAP) {
            terrainActionMap[tm.type] = tm;
        }
    }

    public Color getColor(TerrainSquare.TERRAIN_TYPE type) {
        return terrainColorMap[type];
    }

    public MineAction getMineAction(TerrainSquare.TERRAIN_TYPE type) {
        return terrainActionMap[type];
    }
}

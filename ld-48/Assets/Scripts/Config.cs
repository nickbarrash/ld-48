using System;
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
    public List<Enemy> enemy;
}

public class Config : MonoBehaviour
{
    public List<TypeColor> COLOR_MAP;
    public List<MineAction> MINE_ACTION_MAP;

    public Color DANGER_LOW;
    public Color DANGER_HIGH;

    public Color LOOT_LOW;
    public Color LOOT_HIGH;

    public Color BIO_LOW;
    public Color BIO_HIGH;

    public Color ACTIVE_DISPLAY_TYPE_COLOR;
    public Color INACTIVE_DISPLAY_TYPE_COLOR;

    Dictionary<TerrainSquare.TERRAIN_TYPE, Color> terrainColorMap = new Dictionary<TerrainSquare.TERRAIN_TYPE, Color>();
    Dictionary<TerrainSquare.TERRAIN_TYPE, MineAction> terrainActionMap = new Dictionary<TerrainSquare.TERRAIN_TYPE, MineAction>();

    Dictionary<TerrainSquare.TERRAIN_TYPE, string> terrainNameMap = new Dictionary<TerrainSquare.TERRAIN_TYPE, string> {
        { TerrainSquare.TERRAIN_TYPE.BALROG, "Balrog" },
        { TerrainSquare.TERRAIN_TYPE.COPPER, "Copper" },
        { TerrainSquare.TERRAIN_TYPE.DENSE_ROCK, "Dense Rock" },
        { TerrainSquare.TERRAIN_TYPE.DIAMOND, "Diamond" },
        { TerrainSquare.TERRAIN_TYPE.EMPTY, "Dirt" },
        { TerrainSquare.TERRAIN_TYPE.GAS_POCKET, "Gas Pocket" },
        { TerrainSquare.TERRAIN_TYPE.GOLD, "Gold" },
        { TerrainSquare.TERRAIN_TYPE.PLATINUM, "Platinum" },
        { TerrainSquare.TERRAIN_TYPE.ROCKMAN, "Rockman" },
        { TerrainSquare.TERRAIN_TYPE.SILVER, "Silver" },
        { TerrainSquare.TERRAIN_TYPE.SPIDER, "Spider" },
        { TerrainSquare.TERRAIN_TYPE.WORM, "Worm" },
    };

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

    public string getTerrainName(TerrainSquare.TERRAIN_TYPE type) {
        return terrainNameMap[type];
    }
}

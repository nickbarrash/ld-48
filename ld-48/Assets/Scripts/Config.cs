using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TypeColor {
    public TerrainSquare.TERRAIN_TYPE type;
    public Color color;
}

public class Config : MonoBehaviour
{
    public List<TypeColor> COLOR_MAP;

    Dictionary<TerrainSquare.TERRAIN_TYPE, Color> terrainColorMap = new Dictionary<TerrainSquare.TERRAIN_TYPE, Color>();

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
    }

    public Color getColor(TerrainSquare.TERRAIN_TYPE type) {
        return terrainColorMap[type];
    }
}

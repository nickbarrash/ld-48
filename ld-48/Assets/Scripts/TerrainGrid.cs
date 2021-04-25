using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGrid : MonoBehaviour {
    public const int GRID_X = 20;
    public const int GRID_Y = 50;

    public bool debugMode = false;
    public GameObject terrainPrefab;
    public TerrainSquare[,] grid;

    TerrainSquare.TERRAIN_TYPE[] type = new TerrainSquare.TERRAIN_TYPE[] {
        TerrainSquare.TERRAIN_TYPE.EMPTY,
        TerrainSquare.TERRAIN_TYPE.DENSE_ROCK,
        TerrainSquare.TERRAIN_TYPE.GAS_POCKET,
        TerrainSquare.TERRAIN_TYPE.COPPER,
        TerrainSquare.TERRAIN_TYPE.SILVER,
        TerrainSquare.TERRAIN_TYPE.GOLD,
        TerrainSquare.TERRAIN_TYPE.PLATINUM,
        TerrainSquare.TERRAIN_TYPE.DIAMOND,
        TerrainSquare.TERRAIN_TYPE.SPIDER,
    };

    [Serializable]
    public class TypeProbability {
        public TerrainSquare.TERRAIN_TYPE type;
        public float p;
    }

    public List<TypeProbability> p0_15;
    public List<TypeProbability> p15_30;
    public List<TypeProbability> p30_45;

    HashSet<string> initiallyExcavated = new HashSet<string> {
        $"10,0", $"10,1", $"10,2", $"10,3", $"11,3", "9,1", "9,0", "11,1" ,"11,0", "10,4", "10,4"
    };

    public TerrainSquare getSquare(int x, int y) {
        if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1)) {
            return null;
        }
        return grid[x, y];
    }

    public void generate() {
        grid = new TerrainSquare[GRID_X, GRID_Y];

        for(int x = 0; x < grid.GetLength(0); x++) {
            for (int y = 0; y < grid.GetLength(1); y++) {
                GameObject newObj = Instantiate(terrainPrefab, transform);
                newObj.name = $"TerrainSquare_{x}_{y}";
                grid[x, y] = newObj.GetComponent<TerrainSquare>();
                grid[x, y].grid = this;
                grid[x, y].x = x;
                grid[x, y].y = y;
                grid[x, y].transform.position = new Vector3(x, y * -1, 0);
            }
        }

        for (int x = 0; x < grid.GetLength(0); x++) {
            for (int y = 0; y < grid.GetLength(1); y++) {
                grid[x, y].spawn(generationStrategy1(y), x, y, initiallyExcavated.Contains($"{x},{y}"));
            }
        }

        for (int x = 0; x < grid.GetLength(0); x++) {
            for (int y = 0; y < grid.GetLength(1); y++) {
                grid[x, y].setAffordances();
            }
        }
    }

    public TerrainSquare.TERRAIN_TYPE generationStrategy1(int y) {
        if (y < 15) {
            return pickFromPList(p0_15);
        } else if (y < 30) {
            return pickFromPList(p15_30);
        } else if (y < 45) {
            return pickFromPList(p30_45);
        } else {
            return TerrainSquare.TERRAIN_TYPE.BALROG;
        }
    }

    private TerrainSquare.TERRAIN_TYPE pickFromPList(List<TypeProbability> ps) {
        float rnd = UnityEngine.Random.value;
        float p = 0;
        foreach (var tp in ps) {
            if (rnd <= tp.p + p) {
                return tp.type;
            }
            p += tp.p;
        }
        return TerrainSquare.TERRAIN_TYPE.EMPTY;
    }

    public TerrainSquare.TERRAIN_TYPE randomGeneration() {
        return type[UnityEngine.Random.Range(0, type.Length)];
    }

    public static int gasConsumed(int y) {
        return y;
    }
}

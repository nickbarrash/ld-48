using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGrid : MonoBehaviour {
    public const int GRID_X = 30;
    public const int GRID_Y = 31;

    public bool debugMode = false;
    public GameObject terrainPrefab;
    public TerrainSquare[,] grid;

    HashSet<string> initiallyExcavated = new HashSet<string> {
        $"15,0", $"15,1", $"15,2", $"15,3", $"14,2", $"16,3",
    };

    public TerrainSquare getSquare(int x, int y) {
        if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1)) {
            return null;
        }
        return grid[x, y];
    }

    public void generate() {
        TerrainSquare.TERRAIN_TYPE[] type = new TerrainSquare.TERRAIN_TYPE[] {
            TerrainSquare.TERRAIN_TYPE.EMPTY,
            TerrainSquare.TERRAIN_TYPE.DENSE_ROCK,
            TerrainSquare.TERRAIN_TYPE.GAS_POCKET,
            TerrainSquare.TERRAIN_TYPE.COPPER,
            TerrainSquare.TERRAIN_TYPE.SILVER,
            TerrainSquare.TERRAIN_TYPE.GOLD,
            TerrainSquare.TERRAIN_TYPE.PLATINUM,
            TerrainSquare.TERRAIN_TYPE.DIAMOND,
        };

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
                grid[x, y].spawn(type[Random.Range(0, type.Length)], x, y, initiallyExcavated.Contains($"{x},{y}"));
            }
        }

        for (int x = 0; x < grid.GetLength(0); x++) {
            for (int y = 0; y < grid.GetLength(1); y++) {
                grid[x, y].setAffordances();
            }
        }
    }
}

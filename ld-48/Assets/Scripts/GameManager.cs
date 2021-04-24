using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    TerrainGrid grid;
    CameraManager cameraManager;

    public const int X_OFF = TerrainGrid.GRID_X / 2;
    public const int Y_OFF = -2;

    public bool DEBUG;

    public void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogWarning("Destroying dup game manager");
            Destroy(this);
        }

        grid = FindObjectOfType<TerrainGrid>();
        cameraManager = FindObjectOfType<CameraManager>();
    }

    private void Start() {
        grid.generate();
        cameraManager.setPosition(X_OFF, Y_OFF);
    }
}

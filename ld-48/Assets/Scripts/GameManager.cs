using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    TerrainGrid grid;
    CameraManager camera;

    public void Awake() {
        grid = FindObjectOfType<TerrainGrid>();
        camera = FindObjectOfType<CameraManager>();
    }

    private void Start() {
        grid.generate();
        camera.setPosition(TerrainGrid.X_OFF, TerrainGrid.Y_OFF);
    }
}

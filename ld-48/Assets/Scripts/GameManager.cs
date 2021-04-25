using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    const float SCROLL_FACTOR = 50f;

    public CombatDisplay combatDisplay;

    TerrainGrid grid;
    CameraManager cameraManager;
    Camera mainCam;
    [HideInInspector]
    public Player player;

    public const int X_OFF = TerrainGrid.GRID_X / 2;
    public const int Y_OFF = -2;

    public bool DEBUG;

    public bool displayDanger = true;

    Vector3 mouseDownPos;

    public void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogWarning("Destroying dup game manager");
            Destroy(this);
        }

        mainCam = Camera.main;

        grid = FindObjectOfType<TerrainGrid>();
        player = FindObjectOfType<Player>();
        cameraManager = FindObjectOfType<CameraManager>();
    }

    private void Start() {
        grid.generate();
        cameraManager.setPosition(X_OFF, Y_OFF);
    }

    public void toggleDisplayType() {
        setDisplayType(!displayDanger);
    }

    public void setDisplayType(bool isDanger) {
        displayDanger = isDanger;

        foreach(var dvm in FindObjectsOfType<DisplayValueManager>()) {
            dvm.showValue(displayDanger);
        }
    }

    public void gameOver() {
        Debug.Log("GAME OVER!!");
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            mouseDownPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)) {
            var nextMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            var delta = nextMousePos - mouseDownPos;
            mouseDownPos = nextMousePos;
            grid.transform.position += new Vector3(delta.x, delta.y, 0);
        }
        
        if (Input.mouseScrollDelta.y != 0) {
            mainCam.orthographicSize *= 1 - (Input.mouseScrollDelta.y / SCROLL_FACTOR);
            if (mainCam.orthographicSize <= 2) {
                mainCam.orthographicSize = 2;
            } else if (mainCam.orthographicSize >= 15) {
                mainCam.orthographicSize = 15;
            }
        }
    }

    public void startCombat(Enemy enemy) {
        combatDisplay.startCombat(enemy);
    }
}

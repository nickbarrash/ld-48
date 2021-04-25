using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum CLICK_ACTION_TYPE {
        EXCAVATE,
        SCAN,
        BOMB
    }

    public static GameManager instance;

    const float SCROLL_FACTOR = 50f;

    public CombatDisplay combatDisplay;
    public CatalogDisplay catalogDisplay;

    TerrainGrid grid;
    CameraManager cameraManager;
    Camera mainCam;
    [HideInInspector]
    public Player player;

    public CLICK_ACTION_TYPE actionType = CLICK_ACTION_TYPE.EXCAVATE;

    public const int X_OFF = TerrainGrid.GRID_X / 2 + 1;
    public const int Y_OFF = -3;

    public bool DEBUG;

    [HideInInspector]
    public DisplayValueManager.DISPLAY_VALUE_TYPE displayType = DisplayValueManager.DISPLAY_VALUE_TYPE.DANGER;

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
        combatDisplay.gameObject.SetActive(false);

        catalogDisplay.setPanel(0);
        catalogDisplay.gameObject.SetActive(false);
    }

    public void setExcavationAction() {
        actionType = CLICK_ACTION_TYPE.EXCAVATE;
    }

    public void setBombAction() {
        actionType = CLICK_ACTION_TYPE.BOMB;
    }

    public void setScanAction() {
        actionType = CLICK_ACTION_TYPE.SCAN;
    }

    public void setDisplayTypeBio() {
        setDisplayType(DisplayValueManager.DISPLAY_VALUE_TYPE.BIO);
    }

    public void setDisplayTypeLoot() {
        setDisplayType(DisplayValueManager.DISPLAY_VALUE_TYPE.LOOT);
    }

    public void setDisplayTypeDanger() {
        setDisplayType(DisplayValueManager.DISPLAY_VALUE_TYPE.DANGER);
    }

    public void setDisplayType(DisplayValueManager.DISPLAY_VALUE_TYPE type) {
        displayType = type;
        foreach (var dvm in FindObjectsOfType<DisplayValueManager>()) {
            dvm.showValue(displayType);
        }
    }

    public void gameOver() {
        Debug.Log("GAME OVER!!");
    }

    public void startCombat(Enemy enemy) {
        combatDisplay.startCombat(enemy);
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
}

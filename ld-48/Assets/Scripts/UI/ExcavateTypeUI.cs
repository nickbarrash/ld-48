using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExcavateTypeUI : MonoBehaviour
{
    public Button excavate;
    public Button scan;
    public Button bomb;

    private void Start() {
        setSelectedExcavation();
    }

    public void setSelectedExcavation() {
        excavate.colors = inactive(excavate.colors);
        scan.colors = inactive(scan.colors);
        bomb.colors = inactive(bomb.colors);

        if (GameManager.instance.actionType == GameManager.CLICK_ACTION_TYPE.EXCAVATE) {
            excavate.colors = active(excavate.colors);
            ToastManager.instance.showMessage("Terrain tiles are now being excavated. Excavating may earn you money, damage you, or put you in combat.");
        } else if (GameManager.instance.actionType == GameManager.CLICK_ACTION_TYPE.SCAN) {
            scan.colors = active(scan.colors);
            ToastManager.instance.showMessage("Terrain tiles are now being scanned. Scanning reveals the contents of the tile without triggering its effects");
        } else if (GameManager.instance.actionType == GameManager.CLICK_ACTION_TYPE.BOMB) {
            ToastManager.instance.showMessage("Terrain tiles are now being demolished. Demolishing a tile excavates it without triggering any effects (including earning money)");
            bomb.colors = active(scan.colors);
        }
    }

    private ColorBlock active(ColorBlock inBlock) {
        inBlock.normalColor = Config.instance.ACTIVE_DISPLAY_TYPE_COLOR;
        return inBlock;
    }

    private ColorBlock inactive(ColorBlock inBlock) {
        inBlock.normalColor = Config.instance.INACTIVE_DISPLAY_TYPE_COLOR;
        return inBlock;
    }
}

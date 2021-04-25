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
        } else if (GameManager.instance.actionType == GameManager.CLICK_ACTION_TYPE.SCAN) {
            scan.colors = active(scan.colors);
        } else if (GameManager.instance.actionType == GameManager.CLICK_ACTION_TYPE.BOMB) {
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

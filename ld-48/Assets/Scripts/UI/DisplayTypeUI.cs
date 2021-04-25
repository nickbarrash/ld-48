using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTypeUI : MonoBehaviour
{
    public Button danger;
    public Button loot;
    public Button bio;

    private void Start() {
        setSelectedType();
    }

    public void setSelectedType() {
        danger.colors = inactive(danger.colors);
        loot.colors = inactive(loot.colors);
        bio.colors = inactive(bio.colors);

        if (GameManager.instance.displayType == DisplayValueManager.DISPLAY_VALUE_TYPE.DANGER) {
            danger.colors = active(danger.colors);
        } else if (GameManager.instance.displayType == DisplayValueManager.DISPLAY_VALUE_TYPE.LOOT) {
            loot.colors = active(loot.colors);
        } else if (GameManager.instance.displayType == DisplayValueManager.DISPLAY_VALUE_TYPE.BIO) {
            bio.colors = active(bio.colors);
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

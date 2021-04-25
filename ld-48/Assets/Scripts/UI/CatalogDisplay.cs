using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatalogDisplay : MonoBehaviour
{
    const int LOOT_VALUE = 0;
    const int BIO_VALUE = 1;
    const int GEO_VALUE = 2;

    public GameObject lootPanel;
    public GameObject bioPanel;
    public GameObject geoPanel;

    public Button lootButton;
    public Button bioButton;
    public Button geoButton;

    public void show() {
        gameObject.SetActive(true);
    }

    public void hide() {
        gameObject.SetActive(false);
    }

    public void setPanel(int panel) {
        lootPanel.SetActive(false);
        bioPanel.SetActive(false);
        geoPanel.SetActive(false);

        lootButton.colors = inactive(lootButton.colors);
        bioButton.colors = inactive(bioButton.colors);
        geoButton.colors = inactive(geoButton.colors);

        if (panel == LOOT_VALUE) {
            lootButton.colors = active(lootButton.colors);
            lootPanel.SetActive(true);
        } else if (panel == BIO_VALUE) {
            bioButton.colors = active(bioButton.colors);
            bioPanel.SetActive(true);
        } else {
            geoButton.colors = active(geoButton.colors);
            geoPanel.SetActive(true);
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

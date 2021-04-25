using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayValueManager : MonoBehaviour
{
    public enum DISPLAY_VALUE_TYPE {
        DANGER,
        LOOT,
        BIO
    }

    public TMP_Text dangerValue;
    public TMP_Text lootValue;
    public TMP_Text bioValue;

    private void Awake() {
        showValue(DISPLAY_VALUE_TYPE.DANGER);
    }

    public void hide() {
        gameObject.SetActive(false);
    }

    public void showValue(DISPLAY_VALUE_TYPE type) {
        gameObject.SetActive(true);

        dangerValue.gameObject.SetActive(false);
        lootValue.gameObject.SetActive(false);
        bioValue.gameObject.SetActive(false);

        if (type == DISPLAY_VALUE_TYPE.DANGER) {
            dangerValue.gameObject.SetActive(true);
        } else if (type == DISPLAY_VALUE_TYPE.LOOT) {
            lootValue.gameObject.SetActive(true);
        } else if (type == DISPLAY_VALUE_TYPE.BIO) {
            bioValue.gameObject.SetActive(true);
        }
    }

    public void setValues(int danger, int loot, int bio) {
        dangerValue.color = Color.Lerp(Config.instance.DANGER_LOW, Config.instance.DANGER_HIGH, danger / 6f);
        dangerValue.text = $"{danger}";

        lootValue.color = Color.Lerp(Config.instance.LOOT_LOW, Config.instance.LOOT_HIGH, loot / 6f);
        lootValue.text = $"{loot}";

        bioValue.color = Color.Lerp(Config.instance.BIO_LOW, Config.instance.BIO_HIGH, loot / 6f);
        bioValue.text = $"{bio}";
    }
}

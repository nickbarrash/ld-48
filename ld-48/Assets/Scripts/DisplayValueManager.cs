using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayValueManager : MonoBehaviour
{
    public TMP_Text dangerValue;
    public TMP_Text lootValue;

    private void Awake() {
        showValue(true);
    }

    public void hide() {
        gameObject.SetActive(false);
    }

    public void showValue(bool isDanger) {
        gameObject.SetActive(true);
        dangerValue.gameObject.SetActive(isDanger);
        lootValue.gameObject.SetActive(!isDanger);
    }

    public void setValues(int danger, int loot) {
        dangerValue.color = Color.Lerp(Config.instance.DANGER_LOW, Config.instance.DANGER_HIGH, danger / 6f);
        dangerValue.text = $"{danger}";

        lootValue.color = Color.Lerp(Config.instance.LOOT_LOW, Config.instance.LOOT_HIGH, loot / 6f);
        lootValue.text = $"{loot}";
    }
}

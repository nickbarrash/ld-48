using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayTypeUI : MonoBehaviour
{
    public TMP_Text typeLabel;

    string DANGER_TEXT = "Dangers";
    string LOOT_TEXT = "Loot";

    public void setTypeLabel() {
        string text = GameManager.instance.displayDanger ? DANGER_TEXT : LOOT_TEXT;
        typeLabel.text = $"Displaying:\nAdjacent {text}";
    }
}

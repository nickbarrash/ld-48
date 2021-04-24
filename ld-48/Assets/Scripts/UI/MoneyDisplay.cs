using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    Player player;
    public TMP_Text moneylabel;

    private void Awake() {
        player = FindObjectOfType<Player>();
    }

    public void updateMoney() {
        moneylabel.text = $"${player.money}";
    }
}

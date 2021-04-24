using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasDisplay : MonoBehaviour
{
    Player player;
    public Slider gasSlider;

    private void Awake() {
        player = FindObjectOfType<Player>();
    }

    public void updateGas() {
        gasSlider.value = Mathf.InverseLerp(0, player.maxGas, player.gas);
    }
}

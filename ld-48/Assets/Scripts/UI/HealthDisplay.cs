using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    Player player;
    public Slider healthSlider;

    private void Awake() {
        player = FindObjectOfType<Player>();
    }

    public void updateHealth() {
        healthSlider.value = Mathf.InverseLerp(0, player.maxHealth, player.health);
    }
}

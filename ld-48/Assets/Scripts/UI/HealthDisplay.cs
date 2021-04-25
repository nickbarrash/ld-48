using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthLabel;

    public void updateHealth(int max, int current) {
        healthSlider.value = Mathf.InverseLerp(0, max, current);
        healthLabel.text = $"{current}/{max}";
    }
}

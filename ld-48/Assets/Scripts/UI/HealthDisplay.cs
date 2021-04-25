using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Slider healthSlider;

    public void updateHealth(int max, int current) {
        healthSlider.value = Mathf.InverseLerp(0, max, current);
    }
}

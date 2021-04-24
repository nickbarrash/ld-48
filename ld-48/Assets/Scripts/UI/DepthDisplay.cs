using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DepthDisplay : MonoBehaviour
{
    Player player;
    public TMP_Text depthLabel;

    private void Awake() {
        player = FindObjectOfType<Player>();
    }

    public void updateDepth() {
        depthLabel.text = $"{player.depth}m";
    }
}

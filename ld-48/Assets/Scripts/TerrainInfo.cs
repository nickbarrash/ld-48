using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TerrainInfo : MonoBehaviour
{
    TerrainInfoTile fadeManager;
    public TMP_Text countLabel;
    public SpriteRenderer terrainColor;

    Vector3 defaultScale;

    private void Awake() {
        fadeManager = transform.parent.GetComponent<TerrainInfoTile>();
        defaultScale = terrainColor.gameObject.transform.localScale;
    }

    public void setScale(bool isFullWidth, bool isFullHeight) {
        terrainColor.gameObject.transform.localScale = new Vector3(
            isFullWidth ? defaultScale.x * 2 : defaultScale.x,
            isFullHeight ? defaultScale.y * 2 : defaultScale.y,
            1
        );
    }

    public void setInfo(int count, Color color) {
        countLabel.text = $"{count}";
        terrainColor.color = color;
    }

    public void setOpacity(float opacity) {
        countLabel.color = new Color(countLabel.color.r, countLabel.color.g, countLabel.color.b, opacity > 0.5 ? 1 : 0);
        terrainColor.color = new Color(terrainColor.color.r, terrainColor.color.g, terrainColor.color.b, opacity);
    }

    public void fadeToMe() {
        fadeManager.startFade(this);
    }

    public void snapToMe() {
        fadeManager.snap(this);
    }
}

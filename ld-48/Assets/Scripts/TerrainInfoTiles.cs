using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainInfoTiles : MonoBehaviour
{
    public List<TerrainInfoTile> tiles;
    public List<TerrainInfo> terrainInfos;
    List<KeyValuePair<TerrainSquare.TERRAIN_TYPE, int>> neighborInfos = new List<KeyValuePair<TerrainSquare.TERRAIN_TYPE, int>>();

    int rotationStep;

    float ROTATION_STEP_DURATION = 1f;
    bool isRotating = false;

    KeyValuePair<TerrainSquare.TERRAIN_TYPE, int> tmpInfo;

    public void showTileInfo(bool isShow) {
        gameObject.SetActive(isShow);
    }

    public void setTerrainInfo(IEnumerable<KeyValuePair<TerrainSquare.TERRAIN_TYPE, int>> newNeighborInfos) {
        neighborInfos = newNeighborInfos.ToList();

        foreach (var kv in neighborInfos) {
            Debug.Log($"{transform.parent.parent.name} - {kv.Key}: {kv.Value}");
        }
        Debug.Log($"{transform.parent.parent.name} - infos: {neighborInfos.Count}, usable: {usableTiles()}");

        // setup tile location
        for (int i = 0; i < usableTiles(); i++) {
            tiles[i].setPosition(i, usableTiles());
            tiles[i].setScale(i, usableTiles());
        }
        for (int i = usableTiles(); i < tiles.Count; i++) {
            tiles[i].hide();
        }

        // set tile info and color
        for (int i = 0; i < usableTiles(); i++) {
            terrainInfos[i].setInfo(neighborInfos[i].Value, Config.instance.getColor(neighborInfos[i].Key));
        }

        snapInfoToA();
    }

    public int usableTiles() {
        return Mathf.Min(neighborInfos.Count, tiles.Count);
    }

    private void snapInfoToA() {
        for (int i = 0; i < usableTiles(); i++) {
            terrainInfos[i].snapToMe();
        }

        rotationStep = 0;
    }

    private IEnumerator delayedStartRotation() {
        isRotating = true;
        yield return new WaitForSeconds(ROTATION_STEP_DURATION);
        startRotation();
        isRotating = false;
    }

    private void startRotation() {
        tmpInfo = neighborInfos[(++rotationStep + tiles.Count) % neighborInfos.Count];
        terrainInfos[rotationStep % terrainInfos.Count].setInfo(tmpInfo.Value, Config.instance.getColor(tmpInfo.Key));
        terrainInfos[rotationStep % terrainInfos.Count].fadeToMe();
    }

    private void Update() {
        if (neighborInfos.Count > tiles.Count) {
            if (!isRotating) {
                StartCoroutine(delayedStartRotation());
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainInfoTile : MonoBehaviour
{
    public TerrainInfo A;
    public TerrainInfo B;

    float FADE_TIME = 0.5f;
    float fadeProgress;
    bool fading = false;
    TerrainInfo currentlyShowing;

    Vector3 POSITION_OFFSETS = new Vector3(112.5f, 112.5f, 0);

    private void Awake() {
        currentlyShowing = B;
        snap(A);
    }

    public void hide() {
        gameObject.SetActive(false);
    }

    public void setPosition(int index, int count) {
        //Debug.Log($"{transform.parent.parent.parent.gameObject.name}/{gameObject.name} setPosition ({index}, {count})");
        if (count <= 1) {
            transform.localPosition = new Vector3(0, 0, 0);
        } else if (count == 2) {
            transform.localPosition = new Vector3(0, POSITION_OFFSETS.y * (index == 0 ? 1 : -1), 0);
        } else if (count == 3) {
            if (index == 0) {
                transform.localPosition = new Vector3(0, POSITION_OFFSETS.y, 0);
            } else if (index == 1) {
                transform.localPosition = new Vector3(POSITION_OFFSETS.x * -1, POSITION_OFFSETS.y * -1, 0);
            } else if (index == 2) {
                transform.localPosition = new Vector3(POSITION_OFFSETS.x, POSITION_OFFSETS.y * -1, 0);
            } else {
                throw new System.Exception($"unexpected index in set position: {index} (count: {count})");
            }
        } else if (count >= 4) {
            transform.localPosition = new Vector3((index % 2 == 0 ? -1 : 1) * POSITION_OFFSETS.x, (index / 2 == 0 ? 1 : -1) * POSITION_OFFSETS.y, 0);
            Debug.Log(transform.localPosition);
        }
    }

    public void setScale(int index, int count) {
        //Debug.Log($"{transform.parent.parent.parent.gameObject.name}/{gameObject.name} setScale ({index}, {count})");
        var isfullWidth = count <= 2 || count == 3 && index == 0;
        A.setScale(isfullWidth, count == 1);
        B.setScale(isfullWidth, count == 1);
    }

    public void startFade(TerrainInfo fadeTo) {
        if (fadeTo == currentlyShowing) {
            // no fade necessary
            return;
        }

        currentlyShowing = fadeTo;
        fadeProgress = FADE_TIME;
        fading = true;
    }

    public void snap(TerrainInfo snapTo) {
        if (snapTo == currentlyShowing) {
            // no snap necessary
            return;
        }

        currentlyShowing = snapTo;
        A.setOpacity(snapTo == A ? 1 : 0);
        B.setOpacity(snapTo == A ? 0 : 1);
    }

    private void Update() {
        if (fading) {
            fadeProgress -= Time.deltaTime;
            if (fadeProgress <= 0) {
                fading = false;
            }

            float aOpacity = Mathf.InverseLerp(FADE_TIME, 0, fadeProgress);
            if (!(currentlyShowing == A)) {
                aOpacity = 1 - aOpacity;
            }

            A.setOpacity(Mathf.Lerp(0, 1, aOpacity));
            B.setOpacity(Mathf.Lerp(0, 1, 1 - aOpacity));
        }
    }
}

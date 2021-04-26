using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class TypeSprite {
    public TerrainSquare.TERRAIN_TYPE type;
    public SpriteRenderer sprite;
}

public class ContentsDisplay : MonoBehaviour
{
    public List<TypeSprite> typeSpriteMap;
    SpriteRenderer mySprite = null;

    public Dictionary<TerrainSquare.TERRAIN_TYPE, SpriteRenderer> typeSpriteDict;

    public void setContents(TerrainSquare.TERRAIN_TYPE type) {
        foreach (var ts in typeSpriteMap) {
            if (ts.type == type) {
                mySprite = ts.sprite;
            } else {
                if (ts.sprite != null) {
                    Destroy(ts.sprite.gameObject);
                }
            }
        }
    }


    public void hideSprite() {
        if (mySprite != null) {
            mySprite.gameObject.SetActive(false);
        }
    }

    public void displaySprite(TerrainSquare.TERRAIN_TYPE type) {
        if (typeSpriteDict == null) {
            typeSpriteDict = new Dictionary<TerrainSquare.TERRAIN_TYPE, SpriteRenderer>();
            foreach (var ts in typeSpriteMap) {
                if (ts.sprite != null) {
                    typeSpriteDict[ts.type] = ts.sprite;
                }
            }
        }

        if (typeSpriteDict.ContainsKey(type)) {
            mySprite = typeSpriteDict[type];
            mySprite.gameObject.SetActive(true);
        }
    }

    public void displaySprite(bool isShow) {
        if (mySprite != null) {
            mySprite.gameObject.SetActive(isShow);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreDisplay : MonoBehaviour
{
    private void Start() {
        setVisible(false);
    }

    public void setVisible(bool isVisible) {
        gameObject.SetActive(isVisible);
    }
}

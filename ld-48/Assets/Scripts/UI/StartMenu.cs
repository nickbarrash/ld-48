using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject menu;

    public void hide() {
        menu.SetActive(false);
    }
}

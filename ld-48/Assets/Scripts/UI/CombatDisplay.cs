﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);   
    }

    public void startCombat(Enemy enemy) {
        gameObject.SetActive(true);
    }
}

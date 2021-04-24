using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Enemy
{
    public string name;
    public int health;
    public List<Attack> attacks;

    public static Enemy spider() {
        return new Enemy {
            name = "Spider",
            health = 30,
            attacks = new List<Attack> { Attack.bite() }
        };
    }
}

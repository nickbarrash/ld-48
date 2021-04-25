using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attack
{
    public string attackName;
    public string description;
    public int enemyDamage;
    public int selfDamage;
    public int cooldown;
    public List<StatusEffect> effects;
    public int killDamageBonus;

    public static Attack bite() {
        return new Attack {
            attackName = "Bite",
            description = "Damage enemy",
            enemyDamage = 5,
            selfDamage = 0,
            cooldown = 0,
            effects = new List<StatusEffect>(),
            killDamageBonus = 1
        };
    }

    public static Attack drill() {
        return new Attack {
            attackName = "Drill",
            description = "Damage enemy, if the enemy is killed, this attacks' damage is increased by 1",
            enemyDamage = 10,
            selfDamage = 0,
            cooldown = 0,
            effects = new List<StatusEffect>(),
            killDamageBonus = 1
        };
    }

    public static Attack earthquake() {
        return new Attack {
            attackName = "Earthquake",
            description = "Increase attack damage of all combatants by 4x for 3 turns (enemies included!)",
            enemyDamage = 0,
            selfDamage = 0,
            cooldown = 4,
            effects = new List<StatusEffect> {
                new StatusEffect {
                    activeTurns = 3,
                    enemyAttackMultiple = 4,
                    selfAttackMultiple = 4,
                }
            },
            killDamageBonus = 1
        };
    }

    public static Attack liquefaction() {
        return new Attack {
            attackName = "Earthquake",
            description = "Reduce enemy damage by 5x for 3 turns",
            enemyDamage = 0,
            selfDamage = 0,
            cooldown = 6,
            effects = new List<StatusEffect> {
                new StatusEffect {
                    activeTurns = 3,
                    enemyAttackMultiple = 0.2f,
                    selfAttackMultiple = 1,
                }
            },
            killDamageBonus = 1
        };
    }
}

[Serializable]
public class StatusEffect {
    public int activeTurns;
    public float enemyAttackMultiple;
    public float selfAttackMultiple;
}

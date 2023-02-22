using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermesScript : CharacterScript
{
    public HermesScript()
    {
        CharacterName = "Hermes";
        CharacterDescription = "God of Speed";
        CharacterPassive = "Faster movement speed";
        CharacterSecondary = "Decrease fire rate interval for 2 seconds";

        CharacterHealth = 100;
        CharacterFireRate = 0.25f;
        CharacterDamage = 2.5f;
        CharacterSpeed = 1.25f;
        CharacterID = 4;
    }
}

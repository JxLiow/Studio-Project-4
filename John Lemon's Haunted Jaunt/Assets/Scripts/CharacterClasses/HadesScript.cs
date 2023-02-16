using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadesScript : CharacterScript
{
   public HadesScript()
    {
        CharacterName = "Hades";
        CharacterDescription = "God of the Underworld";
        CharacterPassive = "5% chance to inflict poison on projectile hit";
        CharacterSecondary = "Creates an area of poison that damages nearby enemies";

        CharacterHealth = 100;
        CharacterFireRate = 1.0f;
        CharacterDamage = 10;
        CharacterSpeed = 1.0f;
    }
}

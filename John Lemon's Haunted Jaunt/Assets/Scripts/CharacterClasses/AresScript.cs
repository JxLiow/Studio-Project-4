using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AresScript : CharacterScript
{
    public void AresClass()
    {
        CharacterName = "Ares";
        CharacterDescription = "God of War";
        CharacterPassive = "Increase damage by 20% when health falls below 30%";
        CharacterSecondary = "Increase damage output by 10% for 5 secs";

        CharacterHealth = 100;
        CharacterFireRate = 1.0f;
        CharacterDamage = 10.0f;
        CharacterSpeed = 1.0f;
    }
}

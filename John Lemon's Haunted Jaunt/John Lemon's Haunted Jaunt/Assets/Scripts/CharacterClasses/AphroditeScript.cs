using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AphroditeScript : CharacterScript
{
    public AphroditeScript()
    {
        CharacterName = "Aphrodite";
        CharacterDescription = "Goddess of Love and Beauty";
        CharacterPassive = "Health regeneration (1HP per sec) starts after not receiving any damage for 5 seconds";
        CharacterSecondary = "Steal a selected enemy's health";

        CharacterHealth = 100;
        CharacterFireRate = 0.5f;
        CharacterDamage = 5;
        CharacterSpeed = 1.0f;
    }
}

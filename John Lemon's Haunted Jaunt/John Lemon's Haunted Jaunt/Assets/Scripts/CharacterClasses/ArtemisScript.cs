using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtemisScript : CharacterScript
{
    public ArtemisScript()
    {
        CharacterName = "Artemis";
        CharacterDescription = "Goddess of the Hunt";
        CharacterPassive = "Arrows have a 10% chance to crit";
        CharacterSecondary = "Arrows home on nearby enemy for 10 secs";

        CharacterHealth = 100;
        CharacterFireRate = 1.0f;
        CharacterDamage = 10;
        CharacterSpeed = 1.0f;
    }
}

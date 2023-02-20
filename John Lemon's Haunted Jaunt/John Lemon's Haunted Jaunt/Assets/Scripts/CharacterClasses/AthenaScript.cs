using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AthenaScript : CharacterScript
{
    public AthenaScript()
    {
        CharacterName = "Athena";
        CharacterDescription = "Goddess of Wisdom";
        CharacterPassive = "Has a small shield that rotates around her to deflect any bullets caught";
        CharacterSecondary = "Has a shield with 50HP. When broken, will regenerate after 1 minute";

        CharacterHealth = 100;
        CharacterFireRate = 0.5f;
        CharacterDamage = 5.0f;
        CharacterSpeed = 1.0f;
    }
}

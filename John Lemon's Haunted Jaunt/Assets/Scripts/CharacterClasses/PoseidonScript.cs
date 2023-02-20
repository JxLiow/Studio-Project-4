using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseidonScript : CharacterScript
{
    public void PoseidonClass()
    {
        CharacterName = "Poseidon";
        CharacterDescription = "God of the Sea";
        CharacterPassive = "Leaves behind trails of water that slows enemies down when they step into them";
        CharacterSecondary = "Creates a wave of water that pushes back and dmg enemies";

        CharacterHealth = 100;
        CharacterFireRate = 0.5f;
        CharacterDamage = 5;
        CharacterSpeed = 1.0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusScript : CharacterScript
{
   public void ZeusClass()
    {
        CharacterName = "Zeus";
        CharacterDescription = "God of Thunder and Lightning";
        CharacterPassive = "Enemies take 10 damage and are stunned for 1 second everytime they touch the user (Cooldown: 30secs)";
        CharacterSecondary = "Strikes lightning down on mouse location that deals 20 damage and stuns for 2 seconds";

        CharacterHealth = 100;
        CharacterFireRate = 0.5f;
        CharacterDamage = 5;
        CharacterSpeed = 1.0f;
    }
}

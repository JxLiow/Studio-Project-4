using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript 
{
    private string characterName;
    private string characterDescription;
    private string characterPassive;
    private string characterSecondary;

    private int characterHealth;
    private float characterFireRate;
    private float characterDamage;
    private float characterSpeed;
    private int characterID;

    public string CharacterName
    {
        get { return characterName; }
        set { characterName = value; }
    }
    public string CharacterDescription
    {
        get { return characterDescription; }
        set { characterDescription = value; }
    }
    public string CharacterPassive
    {
        get { return characterPassive; }
        set { characterPassive = value; }
    }
    public string CharacterSecondary
    {
        get { return characterSecondary; }
        set { characterSecondary = value; }
    }
    public int CharacterHealth
    {
        get { return characterHealth; }
        set { characterHealth = value; }
    }
    public float CharacterFireRate
    {
        get { return characterFireRate; }
        set { characterFireRate = value; }
    }
    public float CharacterDamage
    {
        get { return characterDamage; }
        set { characterDamage = value; }
    }
    public float CharacterSpeed
    {
        get { return characterSpeed; }
        set { characterSpeed = value; }
    }
    public int CharacterID
    {
        get { return characterID; }
        set { characterID = value; }
    }
}

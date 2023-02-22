using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerScript
{
    private string playerName;
    private string playerGodName;
    private CharacterScript playerClass;

    private int playerHealth;
    private float playerFireRate;
    private float playerDamage;
    private float playerSpeed;
    private int playerGodID;
    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }
    public string PlayerGodName
    {
        get { return playerGodName; }
        set { playerGodName = value; }
    }
    public CharacterScript PlayerClass
    {
        get { return playerClass; }
        set { playerClass = value; }
    }
    public int PlayerHealth
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }
    public float PlayerFireRate
    {
        get { return playerFireRate; }
        set { playerFireRate = value; }
    }
    public float PlayerDamage
    {
        get { return playerDamage; }
        set { playerDamage = value; }
    }
    public float PlayerSpeed
    {
        get { return playerSpeed; }
        set { playerSpeed = value; }
    }
    public int PlayerGodID
    {
        get { return playerGodID; }
        set { playerGodID = value; }
    }
}

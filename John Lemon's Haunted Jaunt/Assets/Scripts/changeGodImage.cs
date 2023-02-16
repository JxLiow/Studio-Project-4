using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class changeGodImage : MonoBehaviour
{
     BasePlayerScript newPlayer;
    ZeusScript zeusScript = new ZeusScript();
    AphroditeScript aphroditeScript = new AphroditeScript();
    HadesScript hadesScript = new HadesScript();
    ArtemisScript artemisScript = new ArtemisScript();
    HermesScript hermesScript = new HermesScript();
    PoseidonScript poseidonScript = new PoseidonScript();
    AresScript aresScript = new AresScript();
    AthenaScript athenaScript = new AthenaScript();
    public GameObject Zeus;
    public GameObject Aphrodite;
    public GameObject Hades;
    public GameObject Artemis;
    public GameObject Hermes;
    public GameObject Poseidon;
    public GameObject Ares;
    public GameObject Athena;
    public TMP_Text GodName;
    public TMP_Text GodDescription;
    public TMP_Text GodPassive;
    public TMP_Text GodAbility;
    public TMP_Text GodFireRate;
    public TMP_Text GodDamage;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        newPlayer = new BasePlayerScript();
        newPlayer.PlayerClass = zeusScript;
    }

    public void Update()
    {
        newPlayer.PlayerGodName = newPlayer.PlayerClass.CharacterName;
        newPlayer.PlayerDamage = newPlayer.PlayerClass.CharacterDamage;
        newPlayer.PlayerHealth = newPlayer.PlayerClass.CharacterHealth;
        newPlayer.PlayerFireRate = newPlayer.PlayerClass.CharacterFireRate;
        newPlayer.PlayerSpeed = newPlayer.PlayerClass.CharacterSpeed;
        //Debug.Log("Player class: " + newPlayer.PlayerClass);
        ////Debug.Log("Player damage: " + newPlayer.PlayerDamage);
        ////Debug.Log("Player speed: " + newPlayer.PlayerSpeed);
        ////Debug.Log("Player fire rate: " + newPlayer.PlayerFireRate);

    }
    public void changeImageZeus()
    {
        Zeus.SetActive(true);
        Aphrodite.SetActive(false);
        Hades.SetActive(false);
        Artemis.SetActive(false);
        Hermes.SetActive(false);
        Poseidon.SetActive(false);
        Ares.SetActive(false);
        Athena.SetActive(false);
        GodName.text = zeusScript.CharacterName;
        GodDescription.text = zeusScript.CharacterDescription;
        GodPassive.text = "Passive: " + zeusScript.CharacterPassive; ;
        GodAbility.text = "Skill: " + zeusScript.CharacterSecondary;
        GodFireRate.text = "Fire rate: " + zeusScript.CharacterFireRate;
        GodDamage.text = "Damage: " + zeusScript.CharacterDamage;
        newPlayer.PlayerClass = zeusScript;
        
    }
    public void changeImageAphrodite()
    {
        Zeus.SetActive(false);
        Aphrodite.SetActive(true);
        Hades.SetActive(false);
        Artemis.SetActive(false);
        Hermes.SetActive(false);
        Poseidon.SetActive(false);
        Ares.SetActive(false);
        Athena.SetActive(false);
        GodName.text = aphroditeScript.CharacterName;
        GodDescription.text = aphroditeScript.CharacterDescription;
        GodPassive.text = "Passive: " + aphroditeScript.CharacterPassive; ;
        GodAbility.text = "Skill: " + aphroditeScript.CharacterSecondary;
        GodFireRate.text = "Fire rate: " + aphroditeScript.CharacterFireRate;
        GodDamage.text = "Damage: " + aphroditeScript.CharacterDamage;
        newPlayer.PlayerClass = aphroditeScript;
    }
    public void changeImageHades()
    {
        Zeus.SetActive(false);
        Aphrodite.SetActive(false);
        Hades.SetActive(true);
        Artemis.SetActive(false);
        Hermes.SetActive(false);
        Poseidon.SetActive(false);
        Ares.SetActive(false);
        Athena.SetActive(false);
        GodName.text = hadesScript.CharacterName;
        GodDescription.text = hadesScript.CharacterDescription;
        GodPassive.text = "Passive: " + hadesScript.CharacterPassive; ;
        GodAbility.text = "Skill: " + hadesScript.CharacterSecondary;
        GodFireRate.text = "Fire rate: " + hadesScript.CharacterFireRate;
        GodDamage.text = "Damage: " + hadesScript.CharacterDamage;
        newPlayer.PlayerClass = hadesScript;
    }
    public void changeImageArtemis()
    {
        Zeus.SetActive(false);
        Aphrodite.SetActive(false);
        Hades.SetActive(false);
        Artemis.SetActive(true);
        Hermes.SetActive(false);
        Poseidon.SetActive(false);
        Ares.SetActive(false);
        Athena.SetActive(false);
        GodName.text = artemisScript.CharacterName;
        GodDescription.text = artemisScript.CharacterDescription;
        GodPassive.text = "Passive: " + artemisScript.CharacterPassive; ;
        GodAbility.text = "Skill: " + artemisScript.CharacterSecondary;
        GodFireRate.text = "Fire rate: " + artemisScript.CharacterFireRate;
        GodDamage.text = "Damage: " + artemisScript.CharacterDamage;
        newPlayer.PlayerClass = artemisScript;
    }
    public void changeImageHermes()
    {
        Zeus.SetActive(false);
        Aphrodite.SetActive(false);
        Hades.SetActive(false);
        Artemis.SetActive(false);
        Hermes.SetActive(true);
        Poseidon.SetActive(false);
        Ares.SetActive(false);
        Athena.SetActive(false);
        GodName.text = hermesScript.CharacterName;
        GodDescription.text = hermesScript.CharacterDescription;
        GodPassive.text = "Passive: " + hermesScript.CharacterPassive; ;
        GodAbility.text = "Skill: " + hermesScript.CharacterSecondary;
        GodFireRate.text = "Fire rate: " + hermesScript.CharacterFireRate;
        GodDamage.text = "Damage: " + hermesScript.CharacterDamage;
        newPlayer.PlayerClass = hermesScript;
    }
    public void changeImagePoseidon()
    {
        Zeus.SetActive(false);
        Aphrodite.SetActive(false);
        Hades.SetActive(false);
        Artemis.SetActive(false);
        Hermes.SetActive(false);
        Poseidon.SetActive(true);
        Ares.SetActive(false);
        Athena.SetActive(false);
        GodName.text = poseidonScript.CharacterName;
        GodDescription.text = poseidonScript.CharacterDescription;
        GodPassive.text = "Passive: " + poseidonScript.CharacterPassive; ;
        GodAbility.text = "Skill: " + poseidonScript.CharacterSecondary;
        GodFireRate.text = "Fire rate: " + poseidonScript.CharacterFireRate;
        GodDamage.text = "Damage: " + poseidonScript.CharacterDamage;
        newPlayer.PlayerClass = poseidonScript;
    }
    public void changeImageAres()
    {
        Zeus.SetActive(false);
        Aphrodite.SetActive(false);
        Hades.SetActive(false);
        Artemis.SetActive(false);
        Hermes.SetActive(false);
        Poseidon.SetActive(false);
        Ares.SetActive(true);
        Athena.SetActive(false);
        GodName.text = aresScript.CharacterName;
        GodDescription.text = aresScript.CharacterDescription;
        GodPassive.text = "Passive: " + aresScript.CharacterPassive; ;
        GodAbility.text = "Skill: " + aresScript.CharacterSecondary;
        GodFireRate.text = "Fire rate: " + aresScript.CharacterFireRate;
        GodDamage.text = "Damage: " + aresScript.CharacterDamage;
        newPlayer.PlayerClass = aresScript;
    }
    public void changeImageAthena()
    {
        Zeus.SetActive(false);
        Aphrodite.SetActive(false);
        Hades.SetActive(false);
        Artemis.SetActive(false);
        Hermes.SetActive(false);
        Poseidon.SetActive(false);
        Ares.SetActive(false);
        Athena.SetActive(true);
        GodName.text = athenaScript.CharacterName;
        GodDescription.text = athenaScript.CharacterDescription;
        GodPassive.text = "Passive: " + athenaScript.CharacterPassive; ;
        GodAbility.text = "Skill: " + athenaScript.CharacterSecondary;
        GodFireRate.text = "Fire rate: " + athenaScript.CharacterFireRate;
        GodDamage.text = "Damage: " + athenaScript.CharacterDamage;
        newPlayer.PlayerClass = athenaScript;
    }
    public void startGameButtonClicked()
    {
        PlayerPrefs.SetString("godname", newPlayer.PlayerGodName);
        PlayerPrefs.SetFloat("damage", newPlayer.PlayerDamage);
        PlayerPrefs.SetFloat("firerate", newPlayer.PlayerFireRate);
        PlayerPrefs.SetFloat("speed", newPlayer.PlayerSpeed);
        PhotonNetwork.LoadLevel("MainScene");
    }
}

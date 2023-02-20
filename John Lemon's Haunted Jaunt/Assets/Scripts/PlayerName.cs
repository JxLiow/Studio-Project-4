using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    public TextMeshProUGUI textElement;

    // Start is called before the first frame update
    void Start()
    {
        textElement = GetComponent<TextMeshProUGUI>();
        textElement.SetText(PlayerPrefs.GetString("name", ""));
    }
}

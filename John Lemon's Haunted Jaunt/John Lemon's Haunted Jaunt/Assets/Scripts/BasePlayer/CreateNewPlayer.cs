using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewPlayer : MonoBehaviour
{
    private BasePlayerScript newPlayer;
    // Start is called before the first frame update
    void Start()
    {
        newPlayer = new BasePlayerScript();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

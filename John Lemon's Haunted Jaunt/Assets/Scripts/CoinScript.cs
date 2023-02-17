using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Photon.Pun;
public class CoinScript : MonoBehaviour
{
    public float timeRemaining = 20;
    bool runTimer = false;

    void Start()
    {
        transform.position = new Vector3(3.0f, 6.0f, 3.0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            runTimer = true;
        }

    }

    void Update()
    {

        if (runTimer == true)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else if (timeRemaining <= 0)
            {
                transform.position = new Vector3(3.0f, 6.0f, 3.0f);
                runTimer = false;
                timeRemaining = 20;
            }
        }

    }

}

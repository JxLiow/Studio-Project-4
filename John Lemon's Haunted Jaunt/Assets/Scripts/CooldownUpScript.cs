using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownUpScript : MonoBehaviour
{
    public float timeRemaining = 2;
    bool runTimer = false;

    private void OnTriggerEnter(Collider other)
    {
        transform.position = new Vector3(0.0f, 0.0f, 0.0f);

        runTimer = true;
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
                transform.position = new Vector3(0.0f, 6.0f, 0.0f);
                runTimer = false;
                timeRemaining = 2;
            }
        }

    }

}

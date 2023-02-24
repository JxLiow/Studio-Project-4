using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = transform.position.z; // Set the z coordinate to the camera's z position
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            
        }
    }
}

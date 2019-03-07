using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChecker : MonoBehaviour {

    // When ball collides with the platform, platform falls down after half a second
    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Ball")
        {
            // Platformer falls after 0.5 seconds
            Invoke("FallDown", 0.5f); 
            FallDown();
        }
    }

    // Makes the platform fall down
    void FallDown() 
    {
        // Adds gravity to the platform so it falls down
        GetComponentInParent<Rigidbody> ().useGravity = true; 
        GetComponentInParent<Rigidbody>().isKinematic = false;

        // Destroys the platform after 2 seconds when it has fallen down
        Destroy(transform.parent.gameObject, 2f); 
    }
}

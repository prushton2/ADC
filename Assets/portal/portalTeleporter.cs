using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class portalTeleporter : MonoBehaviour
{
    // Start is called before the first frame update

    //Gameobject of entire other portal
    public GameObject otherPortal;
    
    public bool disableTravel = false;
    public int timer = 0;

    void Start()
    {

    }

    void FixedUpdate() {
        timer = Math.Max(timer - 1, 0);

        if(timer == 0) {
            disableTravel = false;
        }
    }

    /*
        detect collision with player
            calculate difference in position and rotation
            add position and rotation to player
            disable the other portals collision until collisionexit
    */

    private void OnTriggerEnter(Collider col) {
        // Debug.Log("Portal ran");
        if(disableTravel) {
            return;
        }
        if(otherPortal == null) {
            return;
        }
        
		// Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;
		// transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        
        otherPortal.transform.GetChild(3).GetComponent<portalTeleporter>().disableTravel = true;
        otherPortal.transform.GetChild(3).GetComponent<portalTeleporter>().timer = 30;
        
        col.gameObject.GetComponent<CharacterController>().enabled = false;

        Vector3 distanceOffset = transform.parent.position - otherPortal.transform.position;
        col.gameObject.transform.position -= distanceOffset;

        Vector3 portalRotationalDifference = otherPortal.transform.eulerAngles - transform.eulerAngles;
        Quaternion lookdir = Quaternion.LookRotation(col.gameObject.transform.forward, Vector3.up);
        col.gameObject.transform.eulerAngles = lookdir.eulerAngles + new Vector3(0, 180, 0) + portalRotationalDifference;

        col.gameObject.GetComponent<CharacterController>().enabled = true;

    }
    void onTriggerExit() {
        disableTravel = false;
    }
}
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
        Debug.Log("Portal ran2");
        
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
        Debug.Log("Portal ran");
        if(disableTravel) {
            return;
        }
        if(otherPortal == null) {
            return;
        }
        

        Vector3 distanceOffset = transform.parent.position - otherPortal.transform.position;

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(transform.rotation, otherPortal.transform.rotation);

		Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
		// Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;
		// transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        
        otherPortal.transform.GetChild(3).GetComponent<portalTeleporter>().disableTravel = true;
        otherPortal.transform.GetChild(3).GetComponent<portalTeleporter>().timer = 30;
        
        col.gameObject.GetComponent<CharacterController>().enabled = false;
        col.gameObject.transform.position -= distanceOffset;
        col.gameObject.GetComponent<CharacterController>().enabled = true;



    }
    void onTriggerExit() {
        disableTravel = false;
    }
}

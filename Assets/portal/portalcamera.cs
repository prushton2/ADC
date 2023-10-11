using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalcamera : MonoBehaviour
{

    public Transform playerCamera;
    public Transform otherPortal;
    public Transform thisPortal;
    // Start is called before the first frame update

    public Vector3 distanceOffset;
    public Vector3 portalRotationalDifference;
    void Start()
    {
        thisPortal = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if(otherPortal == null) {
            return;
        }

        distanceOffset = otherPortal.transform.InverseTransformPoint(playerCamera.position);// - otherPortal.position;
        transform.localPosition = new Vector3(-distanceOffset.x, distanceOffset.y, -distanceOffset.z);

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        portalRotationalDifference = otherPortal.transform.InverseTransformDirection(playerCamera.forward);
        // portalRotationalDifference += (thisPortal.rotation * Quaternion.Inverse(otherPortal.rotation)).eulerAngles;
        
        Quaternion cameraDirection = Quaternion.Euler(new Vector3(portalRotationalDifference.x * 180, portalRotationalDifference.y * 180 + 180f, portalRotationalDifference.z * 180));
        
        transform.localRotation = Quaternion.Euler(playerCamera.forward); //cameraDirection;

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // angularDifferenceBetweenPortalRotations = Quaternion.Angle(thisPortal.rotation, otherPortal.rotation);
        // Debug.Log(angularDifferenceBetweenPortalRotations);

		// Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);

        // // Debug.Log(portalRotationalDifference * new Vector3(1f, 1f, 1f));

		// Vector3 newCameraDirection = portalRotationalDifference * -playerCamera.forward;
        // newCameraDirection.y *= -1;

        // // Debug.Log(transform.rotation);
		// transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}

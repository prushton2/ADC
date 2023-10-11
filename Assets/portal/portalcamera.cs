using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalcamera : MonoBehaviour
{

    public Transform playerCamera;
    public Transform otherPortal;
    public Transform thisPortal;
    // Start is called before the first frame update
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
        Vector3 distanceOffset = playerCamera.position - otherPortal.position;
        transform.position = thisPortal.position + distanceOffset;

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(thisPortal.rotation, otherPortal.rotation);

		Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
		Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward; // + new Vector3(270f, 0f, 0f);
		transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        // transform.rotation.y = transform.rotation.y + 180;
        // transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);
    }
}

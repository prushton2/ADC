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
        distanceOffset = playerCamera.position - otherPortal.position;
        transform.localPosition = /*thisPortal.position + */distanceOffset;

        //the problem is that the rotation of each portal is not taken into account when caculating where the camera should be

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(thisPortal.rotation, otherPortal.rotation);

        Debug.Log(angularDifferenceBetweenPortalRotations);

		Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
		Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;

        Debug.Log(newCameraDirection);

		transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}

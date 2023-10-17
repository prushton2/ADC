using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalcamera : MonoBehaviour
{

    public Transform playerCamera;
    public Transform player;
    public Transform otherPortal;
    public Transform thisPortal;
    // Start is called before the first frame update

    public Vector3 distanceOffset;
    public Vector3 portalRotationalDifference;
    public float playerRotationalDifference;
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

        portalRotationalDifference = otherPortal.transform.eulerAngles - thisPortal.transform.eulerAngles;
        Quaternion lookdir = Quaternion.LookRotation(playerCamera.forward, Vector3.up);
        transform.eulerAngles = lookdir.eulerAngles + new Vector3(0, 180, 0) - portalRotationalDifference;
    }
}

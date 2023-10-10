using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    // Start is called before the first frame update

    // public Camera cameraB;
    public Material cameraMatA;
    public Material cameraMatB;

    public GameObject Portal1;
    public GameObject Portal2;
    public GameObject Portal3;

    public GameObject LinkA;
    public GameObject LinkB;

    void Start()
    {

        setLink(LinkA, LinkB);

        // if(cameraB.targetTexture != null) {
        //     cameraB.targetTexture.Release();
        // }

        // cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        // cameraMatB.mainTexture = cameraB.targetTexture;
    }

    void setLink(GameObject portalA, GameObject portalB) {
        
        portalA.transform.GetChild(0).gameObject.GetComponent<portalcamera>().otherPortal = portalB.transform;
        portalB.transform.GetChild(0).gameObject.GetComponent<portalcamera>().otherPortal = portalA.transform;
        
        Camera camA = portalA.transform.GetChild(0).gameObject.GetComponent<Camera>();
        Camera camB = portalB.transform.GetChild(0).gameObject.GetComponent<Camera>();

        if(camA.targetTexture != null) { camA.targetTexture.Release(); }
        if(camB.targetTexture != null) { camB.targetTexture.Release(); }

        camA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatA.mainTexture = camA.targetTexture;

        camB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatB.mainTexture = camB.targetTexture;

        portalA.transform.GetChild(2).gameObject.GetComponent<Renderer>().material = cameraMatB;
        portalB.transform.GetChild(2).gameObject.GetComponent<Renderer>().material = cameraMatA;

    }
}

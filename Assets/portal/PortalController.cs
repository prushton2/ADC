using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : Executable
{
    // Start is called before the first frame update

    // public Camera cameraB;
    public Material cameraMatA;
    public Material cameraMatB;

    public GameObject LinkA;
    public GameObject LinkB;

    // public GameObject Portal1;
    // public GameObject Portal2;
    // public GameObject Portal3;


    void Start()
    {
        setLink(LinkA, LinkB);
    }

    void setLink(GameObject portalA, GameObject portalB) {
        
        //set the other portals
        portalA.transform.GetChild(0).gameObject.GetComponent<portalcamera>().otherPortal = portalB.transform;
        portalB.transform.GetChild(0).gameObject.GetComponent<portalcamera>().otherPortal = portalA.transform;
        
        //reference cameras
        Camera camA = portalA.transform.GetChild(0).gameObject.GetComponent<Camera>();
        Camera camB = portalB.transform.GetChild(0).gameObject.GetComponent<Camera>();

        //remove any target textures they may have
        if(camA.targetTexture != null) { camA.targetTexture.Release(); }
        if(camB.targetTexture != null) { camB.targetTexture.Release(); }

        //set the camA's target texture to a new render texture based on the screen size and set Camera Mat A's texture to the render texture
        camA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatA.mainTexture = camA.targetTexture;

        //same for b
        camB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatB.mainTexture = camB.targetTexture;

        //set the portal's plane's material to the other portals material
        portalA.transform.GetChild(2).gameObject.GetComponent<Renderer>().material = cameraMatB;
        portalB.transform.GetChild(2).gameObject.GetComponent<Renderer>().material = cameraMatA;

    }

    public override string execute(string[] args) {
        return "";
    }
}

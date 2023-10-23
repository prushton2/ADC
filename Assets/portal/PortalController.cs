using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PortalController : Executable
{
    
    public Material cameraMatA;
    public Material cameraMatB;

    public Material noPortal;

    public GameObject LinkA;
    public GameObject LinkB;

    public GameObject[] Portals;

    public String[] portalHashes = new String[3];

    void Start() {
        System.Random rng = new System.Random();
        
        for(int i = 0; i<Portals.Length; i++) {
            portalHashes[i] = String.Format("{0:X6}", rng.Next(0x1000000));
            Portals[i].transform.GetChild(4).GetComponent<TMP_Text>().text = portalHashes[i];
        }

        if(state == "Linked") {
            setLink(LinkA, LinkB);
        }
        
    }


    void FixedUpdate()
    {
        // if(Input.GetKey("i")) {
        //     setLink(LinkA, LinkB);
        // } else if (Input.GetKey("o")) {
        //     unlink(LinkA, LinkB);
        // }
    }

    public void setLink(GameObject portalA, GameObject portalB) {
        
        //set the other portals
        portalA.transform.Find("cam").gameObject.GetComponent<portalcamera>().otherPortal = portalB.transform;
        portalB.transform.Find("cam").gameObject.GetComponent<portalcamera>().otherPortal = portalA.transform;
        
        //reference cameras
        Camera camA = portalA.transform.Find("cam").gameObject.GetComponent<Camera>();
        Camera camB = portalB.transform.Find("cam").gameObject.GetComponent<Camera>();

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

        portalA.transform.Find("plane").gameObject.SetActive(true);
        portalB.transform.Find("plane").gameObject.SetActive(true);

        portalA.transform.Find("plane").gameObject.GetComponent<Renderer>().material = cameraMatB;
        portalB.transform.Find("plane").gameObject.GetComponent<Renderer>().material = cameraMatA;

        //Link the teleporter scripts
        portalA.transform.Find("back").gameObject.GetComponent<portalTeleporter>().otherPortal = portalB;
        portalB.transform.Find("back").gameObject.GetComponent<portalTeleporter>().otherPortal = portalA;
    }

    public void unlink(GameObject portalA, GameObject portalB) {
        //set the other portals
        portalA.transform.Find("cam").gameObject.GetComponent<portalcamera>().otherPortal = null;
        portalB.transform.Find("cam").gameObject.GetComponent<portalcamera>().otherPortal = null;
                
        //reference cameras
        Camera camA = portalA.transform.Find("cam").gameObject.GetComponent<Camera>();
        Camera camB = portalB.transform.Find("cam").gameObject.GetComponent<Camera>();
        //remove any target textures they may have
        if(camA.targetTexture != null) { camA.targetTexture.Release(); }
        if(camB.targetTexture != null) { camB.targetTexture.Release(); }
        //set the camA's target texture to a new render texture based on the screen size and set Camera Mat A's texture to the render texture

        //set the portal's plane's material to the other portals material
        portalA.transform.Find("plane").gameObject.GetComponent<Renderer>().material = noPortal;
        portalB.transform.Find("plane").gameObject.GetComponent<Renderer>().material = noPortal;
        //Link the teleporter sc`ripts
        portalA.transform.Find("back").gameObject.GetComponent<portalTeleporter>().otherPortal = null;
        portalB.transform.Find("back").gameObject.GetComponent<portalTeleporter>().otherPortal = null;

        portalA.transform.Find("plane").gameObject.SetActive(false);
        portalB.transform.Find("plane").gameObject.SetActive(false);

        LinkA = null;
        LinkB = null;
    }

    public override string execute(string[] args) {
        Debug.Log(args);
        if(args[1] == "link") {

            if(state == "Linked") {
                return "Cannot link when a link already exists.\nUse unlink to remove the link\n";
            }

            for(int i = 0; i<portalHashes.Length; i++) {
                if(portalHashes[i] == args[2].ToUpper()) {
                    LinkA = Portals[i];
                }

                if(portalHashes[i] == args[3].ToUpper()) {
                    LinkB = Portals[i];
                }
            }

            setLink(LinkA, LinkB);
            state = "Linked";
            return "Link Made\n";
        } else if(args[1] == "unlink") {
            unlink(LinkA, LinkB);
            state = "Unlinked";
            return "Link Destroyed\n";
        }
        return "Invalid Command\n";
    }
}

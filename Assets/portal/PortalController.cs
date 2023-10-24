using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PortalController : Executable
{
    public GameObject Player;

    public Material cameraMatA;
    public Material cameraMatB;

    public Material noPortal;

    public GameObject LinkA;
    public GameObject LinkB;

    public GameObject[] Portals;

    public String[] portalHashes = new String[3];

    void Start() {
        System.Random rng = new System.Random();
        Player = GameObject.Find("Player");

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
        if(state == "Linked") {
            if(Vector3.Distance(Player.transform.position, LinkA.transform.position) > 50 && Vector3.Distance(Player.transform.position, LinkB.transform.position) > 50) {
                LinkA.transform.Find("cam").gameObject.SetActive(false);
                LinkB.transform.Find("cam").gameObject.SetActive(false);
            } else {
                LinkA.transform.Find("cam").gameObject.SetActive(true);
                LinkB.transform.Find("cam").gameObject.SetActive(true);
            }
        }
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
        state = "Linked";
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
        state = "Unlinked";
    }

    public override string execute(string[] args) {
        if(args.Length < 2) {
            return "Status: "+state+"\nPortals require power to operate\n";
        }

        if(args[1] == "link") {

            if(state == "Linked") {
                return "Cannot link when a link already exists.\nUse unlink to remove the link\n";
            }

            if(args.Length < 4) {
                return "use portal link ID1 ID2\nthe ID of 2 portals is required to link\n";
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
            return "Link Made\n";
        } else if(args[1] == "unlink") {
            unlink(LinkA, LinkB);
            return "Link Destroyed\n";
        }
        return "Invalid Command\n";
    }
}

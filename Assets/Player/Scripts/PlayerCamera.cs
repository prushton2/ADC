using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerCamera : MonoBehaviour {

    public Player playerScript;
    public Canvas canvas;

    public GameObject healthBar;

    public Transform player;
    float ChgInY = 0;
    public double viewHeight = 1.5;
    private GameObject mgr;

    public int interactRange = 3;
    public bool justInteracted = false;

    void Start () {
        // healthBar = canvas.transform.Find("HealthBar").gameObject;
        playerScript = transform.parent.GetComponent<Player>();
    }

    void Update() {

        if(playerScript.movementLocked) {
            return;
        }

        ChgInY += Input.GetAxisRaw("Mouse Y");
        ChgInY = Math.Clamp(ChgInY, -90, 90);
        
        transform.rotation = Quaternion.Euler(-ChgInY, player.transform.rotation.eulerAngles.y, 0);        
    }

    void FixedUpdate() {

        if(playerScript.movementLocked) {
            return;
        }

        RaycastHit destination;

        if(Input.GetKey("e") && !justInteracted) {
            Physics.Raycast(transform.position, transform.forward, out destination, this.interactRange, ~0);
            try {
                destination.transform.gameObject.GetComponent<Interactable>().interact(playerScript.gameObject);
                justInteracted = true;
            } catch {}
        }

        if(!Input.GetKey("e")) {
            justInteracted = false;
        }

    }
}


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
    public float deltaY = 0;
    public double viewHeight = 1.5;
    private GameObject mgr;

    public int interactRange = 3;
    public bool justInteracted = false;

    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        playerScript = transform.parent.GetComponent<Player>();
    }

    void Update() {

        if(playerScript.movementLocked) {
            return;
        }

        deltaY = transform.eulerAngles.x + -Input.GetAxisRaw("Mouse Y") * playerScript.Sensitivity;
        deltaY = deltaY > 180 ? deltaY -= 360 : deltaY;

        transform.eulerAngles = new Vector3(
            Math.Clamp(deltaY, -90, 90),
            transform.eulerAngles.y, 
            transform.eulerAngles.z
        );
   
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


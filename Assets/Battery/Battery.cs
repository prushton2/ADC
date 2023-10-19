using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Interactable
{
    public float MWh = 100;

    public Material chargedMat;
    public Material emptyMat;

    public override void interact(GameObject go) {
        go.GetComponent<Inventory>().pickup(transform.gameObject);
    }

    public void updateTexture() {
        if(MWh == 0) {
            transform.gameObject.GetComponent<Renderer>().material = emptyMat;
            return;
        }
        transform.gameObject.GetComponent<Renderer>().material = chargedMat;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string itemname = "item.name";
    public virtual void interact(GameObject interacter) {
        Debug.Log("Got it!");
    }
}

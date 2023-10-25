using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Used to randomly place objects across the map for replayability
public class RandomPlacer : MonoBehaviour
{
    public Vector3[] positions;
    public Vector3[] rotations;
    public int choice;
    public bool useLocalPosition = false;
    System.Random rand = new System.Random();

    void Start() {
        replace();
    }

    void Update() {
        // place(choice);
    }

    void replace() {
        choice = rand.Next(0, positions.Length);
        place(choice);
    }
    void place(int choice) {

        transform.position = positions[choice];
        if(useLocalPosition) {
            transform.localPosition = positions[choice];
        }
        
        transform.eulerAngles = rotations[choice];
    }
}
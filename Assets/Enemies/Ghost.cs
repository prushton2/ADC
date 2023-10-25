using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Ghost : MonoBehaviour
{
    // Start is called before the first frame update

    public NavMeshAgent agent;
    public GameObject player;

    public GameObject eye;

    public int outsideLineOfSightTimer = 0;
    public int outsideLineOfSightTimerMax = 60;

    public int smellDistance = 20;

    private RaycastHit destination;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        eye = transform.Find("Eye").gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        outsideLineOfSightTimer++;

        eye.transform.LookAt(player.transform.position);

        eye.transform.localRotation = Quaternion.Euler(new Vector3(
            Math.Clamp(eye.transform.localRotation.eulerAngles.x > 180 ? eye.transform.localRotation.eulerAngles.x - 360 : eye.transform.localRotation.eulerAngles.x, -80, 80),
            Math.Clamp(eye.transform.localRotation.eulerAngles.y > 180 ? eye.transform.localRotation.eulerAngles.y - 360 : eye.transform.localRotation.eulerAngles.y, -45, 45),
            eye.transform.localRotation.eulerAngles.z
        ));

        Physics.Raycast(eye.transform.position, eye.transform.forward, out destination, 50, ~0);
        
        if(destination.transform.gameObject.name == "Player" || Vector3.Distance(transform.position, player.transform.position) < smellDistance) {
            outsideLineOfSightTimer = 0;
        }

        if(outsideLineOfSightTimer < outsideLineOfSightTimerMax) {
            agent.destination = player.transform.position;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Ghost : MonoBehaviour
{
    // Start is called before the first frame update

    public Material black;
    public Material red;

    public NavMeshAgent agent;
    public GameObject player;

    public GameObject eye;

    private int outsideLineOfSightTimer = 61; //the timer for how long since the player has left the ghosts LOS
    public int outsideLineOfSightTimerMax = 60;

    public int smellDistance = 20;

    public int damageOnCollide = 50;
    public int damageCooldown = 60;
    private int internalDamageCooldown = 0; //the counter for how long until damage can be re dealt


    private int InternalInstaKillTimer = 0; //the timer that ticks until the player can be instakilled for being inside a ghost
    public int instaKillTimer = 200;

    private RaycastHit destination;

    private Collider lastCol;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        eye = transform.Find("Eye").gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(InternalInstaKillTimer > instaKillTimer) {
            InternalInstaKillTimer++;
        
            if(InternalInstaKillTimer > instaKillTimer) {
                lastCol.GetComponent<HealthPool>().kill();
            }
        
        }

        internalDamageCooldown--;
        outsideLineOfSightTimer++;

        eye.transform.LookAt(player.transform.position);

        eye.transform.localRotation = Quaternion.Euler(new Vector3(
            Math.Clamp(eye.transform.localRotation.eulerAngles.x > 180 ? eye.transform.localRotation.eulerAngles.x - 360 : eye.transform.localRotation.eulerAngles.x, -80, 80),
            Math.Clamp(eye.transform.localRotation.eulerAngles.y > 180 ? eye.transform.localRotation.eulerAngles.y - 360 : eye.transform.localRotation.eulerAngles.y, -45, 45),
            eye.transform.localRotation.eulerAngles.z
        ));

        
        if(outsideLineOfSightTimer < outsideLineOfSightTimerMax) {
            agent.destination = player.transform.position;
            eye.GetComponent<Renderer>().material = red;
        } else {
            eye.GetComponent<Renderer>().material = black;
        }


        if(!Physics.Raycast(eye.transform.position, eye.transform.forward, out destination, 60, ~0)) {
            return;
        }

        if(destination.transform.gameObject.name == "Player" || Vector3.Distance(transform.position, player.transform.position) < smellDistance) {
            outsideLineOfSightTimer = 0;
        }
    }

    private void OnTriggerEnter(Collider col) {
        if(internalDamageCooldown > 0) {
            return;
        }

        col.gameObject.GetComponent<HealthPool>().dealDamage(damageOnCollide);
        lastCol = col;
        InternalInstaKillTimer++;
        internalDamageCooldown = damageCooldown;
    }

    private void OnTriggerExit(Collider col) {
        InternalInstaKillTimer = 0;
    }
    
}

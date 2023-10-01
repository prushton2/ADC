using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour
{

    //dependencies

    public CharacterController characterController;
    public HealthPool healthPool;

    //movement

    public float speed = 7f;
    public float runMultiplier = 1.5f;

    public float jumpSpeed = 0.16f;
    public float gravity = 1f;

    public float Sensitivity = 1.15f;
    public float deltaX;

    //control

    //grounded, inair;
    public string state = "grounded";
    public int stateProgress = 0;
    public bool movementLocked = false;

    //computational

    public Vector3 moveDirection = Vector3.zero;
    public Vector3 velocity = Vector3.zero;

    void Start() {
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate() {
        //we update the state
        

        switch(state) {
            case "grounded":
                physics();
                break;
            case "inair":
                physics();
                break;
            default:
                break;
        }
        
        characterController.Move(velocity);
        
        if(state == "grounded" || state == "inair") {
            state = characterController.isGrounded ? "grounded" : "inair";
        }
    }

    void Update()
    {
        if(movementLocked) { //needs to be changed sometime soon (its cringe af)
            return;
        }

        //update this here instead because fast

        //We put user-critical things here, things the user controls. This has a higher refresh rate, and the movement is accurate at low frame rates
        switch (state) {
            case "grounded":
                look();
                wasd();
                jump();
                break;
            
            case "inair":
                look();
                wasd();
                break;

            case "dash":
                look();
                dash();
                break;

            default:
                break;
        }

    }

    void look() {
        deltaX += Input.GetAxisRaw("Mouse X");
        transform.rotation = Quaternion.Euler(0, Sensitivity*deltaX, 0);
    }

    void wasd() {
        moveDirection = Vector3.zero; //.x = 0;
        // moveDirection.z = 0;

        if (Input.GetKey("w")) {
            moveDirection.x += transform.forward.x*speed*Time.deltaTime;
            moveDirection.z += transform.forward.z*speed*Time.deltaTime;
        }
        if (Input.GetKey("s")) {
            moveDirection.x += -transform.forward.x*speed*Time.deltaTime;
            moveDirection.z += -transform.forward.z*speed*Time.deltaTime;
        }
        if (Input.GetKey("d")) {
            moveDirection.x += transform.right.x*speed*Time.deltaTime;
            moveDirection.z += transform.right.z*speed*Time.deltaTime;
        }
        if (Input.GetKey("a")) {
            moveDirection.x += -transform.right.x*speed*Time.deltaTime;
            moveDirection.z += -transform.right.z*speed*Time.deltaTime;
        }
        
        if(Input.GetKeyDown("left shift")) {
            state = "dash"; //moveDirection = new Vector3(moveDirection.x * runMultiplier, moveDirection.y, moveDirection.z * runMultiplier);
            stateProgress = 0;
        }

        characterController.Move(moveDirection); //ALL REFERENCES TO MOVEDIRECTION SHOULD INCLUDE TIME.DELTATIME
    }

    void physics() {
        if (!characterController.isGrounded) { //This code modifies the accumulator moveDirection.y, allowing jumping and falling properly
            velocity.y -= gravity;
        } else {// (characterController.isGrounded) {
            velocity.y = -0.1f;
        }
        float drate = 0.1f;
        velocity.x -= drate * velocity.x;
        velocity.z -= drate * velocity.z;
        // velocity -= new Vector3(drate * velocity.x, 0, drate * velocity.z);
    }

    void jump() {
        if(Input.GetKey("space")) {
            velocity.y = jumpSpeed;
        }
    }

    // void hardVelocityFallOff(int rate) {
    //     float drate = rate * 0.01f;
    // }

    void dash() {
        if(Input.GetKey("space")) {
            jump();
            // velocity = new Vector3(hardVelocity.x * 2, hardVelocity.y * 2, hardVelocity.z * 2);
            state = "inair";
            stateProgress = 0;
            // velocity.x = transform.forward.x;
            // velocity.z = transform.forward.z;
        }

        stateProgress += 1;
        // if(stateProgress == 1) {
            velocity.x = transform.forward.x;
            velocity.z = transform.forward.z;
        // }

        if(stateProgress >= 200) {
            velocity = Vector3.zero;
            state = "grounded";
            stateProgress = 0;
        }
    }
}

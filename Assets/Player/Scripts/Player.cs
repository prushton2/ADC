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

    public float jumpSpeed = 0.2f;
    public float gravity = 0.01f;

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

    // void FixedUpdate() {
    //     //we update the state
        

    //     switch(state) {
    //         case "grounded":
    //             physics();
    //             break;
    //         case "inair":
    //             physics();
    //             break;
    //         default:
    //             break;
    //     }
        
    //     characterController.Move(velocity);
        
    //     if(state == "grounded" || state == "inair") {
    //         state = characterController.isGrounded ? "grounded" : "inair";
    //     }
    // }

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

            // case "dash":
            //     look();
            //     dash();
            //     break;

            default:
                break;
        }

    }

    void look() {
        deltaX += Input.GetAxisRaw("Mouse X") * 4;
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
        
        // if(Input.GetKeyDown("left shift")) {
        //     state = "dash"; //moveDirection = new Vector3(moveDirection.x * runMultiplier, moveDirection.y, moveDirection.z * runMultiplier);
        //     stateProgress = 0;
        // }

        characterController.Move(moveDirection); //ALL REFERENCES TO MOVEDIRECTION SHOULD INCLUDE TIME.DELTATIME
    }

    void physics() {
        //horizontal velocity drain rate
        float drate;
        if (!characterController.isGrounded) { //This code modifies the accumulator moveDirection.y, allowing jumping and falling properly
            velocity.y -= gravity;
            drate = 0.01f;
        } else {
            velocity.y = -0.1f;
            drate = 0.3f;
        }

        velocity.x *= 1.0f - drate;
        velocity.z *= 1.0f - drate;
    
        // velocity.x = ;
        // velocity.z = ;
    }

    void jump() {
        if(Input.GetKey("space")) {
            velocity.y = jumpSpeed;
            // Debug.Log("Jump");
        }
    }

    // void dash() {

    //     if(Input.GetKey("space")) {
    //         jump();
    //         int mult = 1000;
    //         velocity.x = transform.forward.x * mult;
    //         velocity.z = transform.forward.z * mult;
    //         state = "inAir";
    //     }

    //     // if(Input.GetKey("space")) {
    //     //     // velocity = new Vector3(hardVelocity.x * 2, hardVelocity.y * 2, hardVelocity.z * 2);
    //     //     int mult = 1;
    //     //     velocity.x = 0; //transform.forward.x * mult;
    //     //     velocity.z = 0; //transform.forward.z * mult;
    //     //     stateProgress = 0;
    //     //     state = "inair";
    //     //     jump();
    //     // }

    //     stateProgress += 1;
    //     // if(stateProgress == 1) {
    //         velocity.x = transform.forward.x;
    //         velocity.z = transform.forward.z;
    //     // }

    //     if(stateProgress >= 200) {
    //         velocity = Vector3.zero;
    //         state = "grounded";
    //         stateProgress = 0;
    //     }
    // }
}

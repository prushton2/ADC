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
    public string state = "grounded";
    public int stateProgress = 0;
    public bool movementLocked = false;

    //computational

    public Vector3 moveDirection = Vector3.zero;
    public Vector3 velocity = Vector3.zero;

    void Start() {
        characterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        if(movementLocked) { //needs to be changed sometime soon (its cringe af)
            return;
        }

        //update this here instead because fast

        //We put user-critical things here, things the user controls. This has a higher refresh rate
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

            default:
                break;
        }
        
        physics();
        characterController.Move(velocity * Time.deltaTime);
        
        if(state == "grounded" || state == "inair") {
            state = characterController.isGrounded ? "grounded" : "inair";
        }
    }

    void look() {
        transform.eulerAngles += new Vector3(0, Input.GetAxisRaw("Mouse X") * Sensitivity, 0);
    }

    void wasd() {
        moveDirection = Vector3.zero; //.x = 0;

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
        
        if(Input.GetKey("left shift")) {
            moveDirection.x *= 1.5f;
            moveDirection.z *= 1.5f;
        }

        characterController.Move(moveDirection); //ALL REFERENCES TO MOVEDIRECTION SHOULD INCLUDE TIME.DELTATIME
    }

    void physics() {
        if (!characterController.isGrounded) { //This code modifies the accumulator moveDirection.y, allowing jumping and falling properly
            velocity.y -= gravity * Time.deltaTime;
        } else {
            velocity.y = -0.1f;
        }
    }

    void jump() {
        if(Input.GetKey("space")) {
            velocity.y = jumpSpeed;
        }
    }
}

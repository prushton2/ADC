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

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 hardVelocity = Vector3.zero;

    void Start() {
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate() {
        //we update the state
        

        //we put physics critical things here, like hard velocity falloff
        switch(state) {
            case "grounded":
                hardVelocityFallOff(30);
                break;
            case "inair":
                hardVelocityFallOff(1);
                break;
        }   
        characterController.Move(hardVelocity);

    }

    void Update()
    {
        if(movementLocked) { //needs to be changed sometime soon (its cringe af)
            return;
        }

        //update this here instead because fast
        if(state == "grounded" || state == "inair") {
            state = characterController.isGrounded ? "grounded" : "inair";
            Debug.Log(characterController.isGrounded);
        }

        //We put user-critical things here, things the user controls. This has a higher refresh rate, and the movement is accurate at low frame rates
        switch (state) {
            case "grounded":
                look();
                wasd();
                jump();
                fall();
                break;
            
            case "inair":
                look();
                wasd();
                fall();
                break;

            case "dash":
                look();
                dash();
                break;

            default:
                break;
        }

        characterController.Move(moveDirection); //ALL REFERENCES TO MOVEDIRECTION SHOULD INCLUDE TIME.DELTATIME
    }

    void look() {
        deltaX += Input.GetAxisRaw("Mouse X");
        transform.rotation = Quaternion.Euler(0, Sensitivity*deltaX, 0);
    }

    void wasd() {
        moveDirection.x = 0;
        moveDirection.z = 0;

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

    }

    void fall() {
        if (!characterController.isGrounded) { //This code modifies the accumulator moveDirection.y, allowing jumping and falling properly
            moveDirection.y -= gravity*Time.deltaTime;
        } else {// (characterController.isGrounded) {
            moveDirection.y = 0;
        }
    }

    void jump() {
        if(Input.GetKey("space")) {
            moveDirection.y = jumpSpeed;
        }
    }

    void hardVelocityFallOff(int rate) {
        float drate = rate * 0.01f;
        hardVelocity -= new Vector3(drate * hardVelocity.x, drate * hardVelocity.y, drate * hardVelocity.z);
    }

    void dash() {
        if(Input.GetKey("space")) {
            jump();
            state = "inair";
            stateProgress = 0;
            hardVelocity = new Vector3(hardVelocity.x * 2, hardVelocity.y * 2, hardVelocity.z * 2);
        }
        stateProgress += 1;
        if(stateProgress == 1) {
            hardVelocity = transform.forward;
        }

        if(stateProgress >= 200) {
            hardVelocity = Vector3.zero;
            state = "grounded";
            stateProgress = 0;
        }
    }

        

}

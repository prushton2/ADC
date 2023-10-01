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

    public bool movementLocked = false;

    //computational

    private Vector3 moveDirection = Vector3.zero;

    void Start() {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {

        if(movementLocked) { //needs to be changed sometime soon (its cringe af)
            return;
        }

        deltaX += Input.GetAxisRaw("Mouse X");
        transform.rotation = Quaternion.Euler(0, Sensitivity*deltaX, 0);

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
        
        if(Input.GetKey("left shift")) {
            moveDirection = new Vector3(moveDirection.x * runMultiplier, moveDirection.y, moveDirection.z * runMultiplier);
        }

        characterController.Move(moveDirection);

        if (!characterController.isGrounded) { //This code modifies the accumulator moveDirection.y, allowing jumping and falling properly
            moveDirection.y -= gravity*Time.deltaTime;
        } else {// (characterController.isGrounded) {
            moveDirection.y = 0;

            if(Input.GetKey("space")) {
                moveDirection.y = jumpSpeed;
            }
        }

        



    }

    void FixedUpdate() {}

}


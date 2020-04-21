using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 10;
    public float jumpForce = 5;
    public float gravityScale = 1;
    
    public CharacterController characterController;
    Vector3 moveDirection;

    void Start()
    {
        //playerRigidbody = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*playerRigidbody.velocity = new Vector3(Input.GetAxis("Horizontal")*moveSpeed, playerRigidbody.velocity.y, Input.GetAxis("Vertical")*moveSpeed);
        if (Input.GetButtonDown("Jump")) {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpForce, playerRigidbody.velocity.z); ;
        }*/
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);

        if (characterController.isGrounded) {
            if (Input.GetButtonDown("Jump")) {
                moveDirection.y = jumpForce;
            }
        }
        moveDirection.y += (Physics.gravity.y * gravityScale);

        characterController.Move(moveDirection*Time.deltaTime);
    }
}

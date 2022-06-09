using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    private Vector3 playerVelocity;

    private bool groundedPlayer;

    public float playerSpeed = 8.0f;

    public float dashSpeed = 100.0f;

    public float jumpHeight = 5.0f;

    private float gravityValue = -9.81f;

    [SerializeField] private  float shiftSpeed;

    private void Awake()
    {
        shiftSpeed = 0;
        playerSpeed = 8.0f * PlayerStats.movementTier;
    }

    void FixedUpdate()
    {
        playerSpeed = 8.0f * PlayerStats.movementTier;

        groundedPlayer = controller.isGrounded;

        shiftSpeed -= Time.deltaTime;

        if (shiftSpeed < 0)
        {
            shiftSpeed = 0.1f;
        }

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -1.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0, y);

        move = transform.TransformDirection(move);

        if (Input.GetKey(KeyCode.LeftShift) && shiftSpeed <= 2 * PlayerStats.dashTier)
        {
            //shiftSpeed += 0.3f * PlayerStats.dashTier;
            shiftSpeed += 10f*Time.deltaTime ;

            if (shiftSpeed < 1.9*PlayerStats.dashTier) {
                controller.Move(move * Time.deltaTime * dashSpeed);
            }
        }
        else 
        {
            controller.Move(move * Time.deltaTime * playerSpeed);
        }
        
        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
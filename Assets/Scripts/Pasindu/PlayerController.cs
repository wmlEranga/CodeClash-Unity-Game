using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;  // speed of movement
    public Vector3 forward, right;  // vectors for forward and right directions
    private bool isJumping = false;  // flag for jump action
    private float jumpHeight = 2.0f;  // height of the jump
    private float jumpTime = 0.5f;  // duration of the jump
    private float jumpTimer = 0.0f;  // timer for the jump
    private bool isMoving = true; // initialize as true to enable movement

    //private ScoreManager scoreManager; // reference to the ScoreManager component    

    public void StopMoving()
    {
        isMoving = false;
    }

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;  // get the right vector from the forward vector

        
        // find the ScoreManager component and store a reference to it
       // scoreManager = FindObjectOfType<ScoreManager>();
    }

    /*void Update()
    {
        if (isMoving)
        {
        // Accept input and move the player
            if (!isJumping)  // don't accept any inputs while jumping
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    Move(forward);
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Move(-right);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    Move(-forward);
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    Move(right);
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
            }
            else  // jump action
            {
                jumpTimer += Time.deltaTime;
                if (jumpTimer < jumpTime * 0.5f)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * jumpHeight, Space.World);  // move up
                }
                else if (jumpTimer < jumpTime)
                {
                    transform.Translate(Vector3.down * Time.deltaTime * jumpHeight, Space.World);  // move down
                }
                else
                {
                    isJumping = false;
                    jumpTimer = 0.0f;
                }
            }
        }  
    }*/

    public void Move(Vector3 direction)
    {
        transform.forward = direction;  // rotate the character to face the moving direction
        float y = 1.5f;  // the desired y-coordinate
        Vector3 raycastOrigin = new Vector3(transform.position.x, y, transform.position.z);  // create a new position with the desired y-coordinate
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, direction, out hit, 1.0f))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                // The player has collided with an obstacle, so stop moving
                Debug.Log("Obstacle detected in front of game object!");
                return;
            }
            else if (hit.collider.CompareTag("Star"))
            {
                // The player has collected a star, so destroy the star object
                Debug.Log("Star detected in front of game object!");
                Destroy(hit.collider.gameObject);
              //  GameManager.instance.IncreaseScore();
            }
        }
        transform.position += direction * speed;
        
    }

    void Jump()
    {
        isJumping = true;
        Vector3 jumpDirection = transform.forward;
        jumpDirection.y = 0;
        jumpDirection = Vector3.Normalize(jumpDirection);
        Vector3 startPos = transform.position;  // store the starting position
        transform.Translate(jumpDirection * 2.0f, Space.World);  // move forward 2 units
        transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);  // reset the y-position to the starting position
        isJumping = false;  // set isJumping to false once the jump is complete
    }
}
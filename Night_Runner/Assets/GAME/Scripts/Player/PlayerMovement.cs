using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : LaneObject
{
    [SerializeField] private float jumpForce;  
    [SerializeField] private float gravityForce;
    [SerializeField] private LayerMask groundLayers;

    private GameManager gameManagerRef;

    private Rigidbody rigidBody;

    private float cooldownBetweeMoves;
    private float eligibleMoveTime;
    private bool isGrounded = true;

    private Player playerRef;
    private void Awake()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        playerRef = gameObject.GetComponent<Player>();

        currentLane = 1;
        cooldownBetweeMoves = 1f / laneChangeSpeed + 0.1f;
    }
    private void Start()
    {
        gameManagerRef = GameManager.Instance;
    }
    private void Update()
    {
        if (playerRef.hasDied)
            return;

        MoveInput();

        IsGrounded();

        if(isGrounded)
        JumpInput();

        SlamInput();

    }
    private void FixedUpdate()
    {
        AddGravity();
    }

    private void MoveInput()
    {
        if (Time.time >= eligibleMoveTime)
        {
            if (Input.GetKeyDown(PlayerKeybindings.leftMove))
            {
                if (currentLane > 0)
                {
                    gameManagerRef.ChangeObjectLane(this, --currentLane);
                    //eligibleMoveTime = Time.time + cooldownBetweeMoves;
                }


            }
            if (Input.GetKeyDown(PlayerKeybindings.rightMove))
            {
                if (currentLane < gameManagerRef.GetLaneCount() - 1)
                {
                    gameManagerRef.ChangeObjectLane(this, ++currentLane);
                    //eligibleMoveTime = Time.time + cooldownBetweeMoves;
                }
            }
        }

    }
    private void JumpInput()
    {
        if(Input.GetKeyDown(PlayerKeybindings.jump))
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }
    }
    private void SlamInput()
    {
        if (Input.GetKeyDown(PlayerKeybindings.slam))
        {
            if(rigidBody.velocity.y > -jumpForce)
            {
                rigidBody.velocity -= Vector3.up * rigidBody.velocity.y;
                rigidBody.AddForce(Vector2.down * jumpForce, ForceMode.Impulse);
            }
        }
    }
    private void IsGrounded()
    {
        bool hitGround = Physics.Raycast(gameObject.transform.position, Vector3.down, 1.1f, groundLayers);

        if(hitGround == true && isGrounded == false)//just hit ground
        {
            rigidBody.velocity -= rigidBody.velocity.y * Vector3.up;
        }

        isGrounded = hitGround;
    }
    private void AddGravity()
    {
        if (isGrounded)
            return;

        rigidBody.velocity -= Vector3.up * Time.fixedDeltaTime * gravityForce;
    }
}

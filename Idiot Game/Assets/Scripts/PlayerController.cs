using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    
    //empty game objects to determine player surrounding
    public Transform groundCheck, groundCheck2, leftWallCheck, rightWallCheck, leftWallCheck2, rightWallCheck2, leftWallCheck3, rightWallCheck3;
    /*
    grounded = player is on ground
    movingEnabled = player is not attached to wall
    onLeftWall/onRightWall = player is next to a given wall
    */
    private bool grounded, movingEnabled, onLeftWall, onRightWall;
    private float jumpHeight, moveSpeed;
    //how far to check around player for wall
    private float wallCheckRadius;
    public LayerMask whatIsGround;
    public Sprite playerLeft, playerRight;
    void Start()
    {
        grounded = true;
        moveSpeed = 3;
        jumpHeight = 5;
        movingEnabled = true;
        wallCheckRadius = .01f;
    }

    // Update is called once per frame
    void Update()
    {
        if (grounded && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)  || Input.GetKey(KeyCode.UpArrow)))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            
        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && movingEnabled && !onLeftWall)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = playerLeft;
        }

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && movingEnabled && !onRightWall)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = playerRight;
        }

        if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) && movingEnabled)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }

        //Is the leftWallCheck transform in contact with a wall?
        onLeftWall = Physics2D.OverlapCircle(leftWallCheck.position, wallCheckRadius, whatIsGround) || Physics2D.OverlapCircle(leftWallCheck2.position, wallCheckRadius, whatIsGround) || Physics2D.OverlapCircle(leftWallCheck3.position, wallCheckRadius, whatIsGround);
        //Is the rightWallCheck transform in contact with a wall?
        onRightWall = Physics2D.OverlapCircle(rightWallCheck.position, wallCheckRadius, whatIsGround) || Physics2D.OverlapCircle(rightWallCheck2.position, wallCheckRadius, whatIsGround) || Physics2D.OverlapCircle(rightWallCheck3.position, wallCheckRadius, whatIsGround);
        
    }
    void FixedUpdate()
    {
        //Is the groundCheck transform in contact with the ground?
        grounded = Physics2D.OverlapCircle(groundCheck.position, wallCheckRadius, whatIsGround) || Physics2D.OverlapCircle(groundCheck2.position, wallCheckRadius, whatIsGround);
    }

    void OnCollisionEnter2D(Collision2D col){
        
    }

    public float JumpHeight { get => JumpHeight1; set => JumpHeight1 = value; }
    public float JumpHeight1 { get => jumpHeight; set => jumpHeight = value; }
    public bool Grounded { get => Grounded1; set => Grounded1 = value; }
    public bool Grounded1 { get => Grounded2; set => Grounded2 = value; }
    public bool Grounded2 { get => Grounded3; set => Grounded3 = value; }
    public bool Grounded3 { get => grounded; set => grounded = value; }
}

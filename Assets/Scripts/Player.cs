using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anime;

    private int facingDir = 1;
    private bool facingR = true;

    [Header("Move info")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool doubleJump = true;

    [Header("Wall info")]
    public Vector3 wallOffSet;
    [SerializeField] private bool isLeftWall;
    [SerializeField] private bool isRightWall;
    [SerializeField] private LayerMask whatIsWall;

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    public enum AnimationState
    {
        playerMove,
        playerJump,
        playerDoubleJump,
        playerGrap,
        none
    }

    AnimationState playerState;

    [SerializeField] private AudioSource jumpSound;

    private bool isGrounded;

    private float xInput;

    //Optimize
    private bool isJumping;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Movement();
        CheckInput();

        AnimationController();
        FlipController();
        CollisionCheck();
        WallCheck();
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            doubleJump = true;
            isJumping = true;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && doubleJump)
        {
            Jump();
            doubleJump = false;
            isJumping = true;
        }

        if (Input.GetKeyUp(KeyCode.Space) || rb.velocity.y < 0)
        {
            isJumping= false;
        }
        if (isJumping)
        {
            rb.velocity += new Vector2(0, -Physics2D.gravity.y * Time.deltaTime);
        }
        if(!isJumping)
        {
            rb.velocity -= new Vector2(0, -Physics2D.gravity.y *  Time.deltaTime);
        }
    }

    private void AnimationController()
    {
        bool isMoving = rb.velocity.x != 0;
        anime.SetFloat("yVelocity", rb.velocity.y);

        anime.SetBool("isMoving", isMoving);
        anime.SetBool("isGrounded", isGrounded);
    }

    private void WallCheck()
    {
        isLeftWall = Physics2D.OverlapCircle(transform.position - wallOffSet, 0.1f, whatIsWall);
        isRightWall = Physics2D.OverlapCircle(transform.position + wallOffSet, 0.1f, whatIsWall);
    }

    private void WallGrap()
    {
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero; 
        playerState = AnimationState.playerGrap;
    }
    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
            rb.velocity = new Vector2(rb.velocity.x * -Time.deltaTime, jumpForce);
            jumpSound.Play();
    }

    private void FlipController()
    {
        if(rb.velocity.x > 0 && !facingR)
        {
            Flip();
        }
        if(rb.velocity.x < 0 && facingR)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDir = facingDir * -1;
        facingR = !facingR;
        transform.Rotate(0, 180, 0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void CollisionCheck()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawWireSphere(transform.position - wallOffSet, 0.1f);
        Gizmos.DrawWireSphere(transform.position + wallOffSet, 0.1f);
    }
}

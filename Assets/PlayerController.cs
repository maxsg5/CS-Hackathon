using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 2D Platformer Controller
[RequireComponent(typeof(Rigidbody2D))] //[RequireComponent(typeof(BoxCollider2D))] this tells unity to add a boxcollider to the object if it doesn't already have one
[RequireComponent(typeof(Animator))] 
[RequireComponent(typeof(AudioSource))] 
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    #region public variables
    //public variables are visible in the inspector by default
    //try to minimize the number of public variables and use private variables with public getters and setters instead when possible
    #endregion

    #region private variables
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;
    private BoxCollider2D boxCollider;
    //[serializefield] makes it visible in the inspector but the variable is still private
    [SerializeField] private float moveSpeed = 5f; 
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private float health = 100f;
    [SerializeField] private float maxHealth = 100f;
    #endregion

    #region unity methods
    //Awake is called before Start
    void Awake()
    {
        //initialize the variables for the components attached to the object
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //initialize variables
    }

    // Update is called once per frame
    void Update()
    {
        //get input
        float horizontal = Input.GetAxis("Horizontal");
        bool jump = Input.GetButtonDown("Jump");
        //move the player based on input
        Move(horizontal);
        //check if the player is grounded
        CheckGrounded();
        //jump if the player is grounded and the jump button is pressed
        if (jump && isGrounded)
        {
            Jump();
        }

    }
    
    #endregion

    #region custom methods
    //custom methods are methods that you create yourself go here
    void Move(float horizontal)
    {
        //move the player with delta time so that the movement is framerate independent
        rb.velocity = new Vector2(horizontal * moveSpeed * Time.deltaTime, rb.velocity.y);
        print(rb.velocity);
        //flip the player if they are moving left
        if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //flip the player if they are moving right
        else if (horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        //set the animator parameters
        // animator.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    void Jump()
    {
        //add force to the player
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        //set the animator parameters
        // animator.SetTrigger("Jump");
    }
    
    void CheckGrounded()
    {
        //check if the player is grounded
        isGrounded = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - boxCollider.bounds.extents.y), new Vector2(boxCollider.bounds.size.x - 0.1f, 0.1f), 0, LayerMask.GetMask("Ground"));
        //set the animator parameters
        // animator.SetBool("Grounded", isGrounded);
    }

    void GetHealth()
    {
        //get the health of the player
    }

    void RemoveHealth(float amount)
    {
        //remove health from the player
    }

    void AddHealth(float amount)
    {
        //add health to the player
    }
    
    #endregion
}

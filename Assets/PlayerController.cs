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
    private Vector3 moveDirection = Vector3.zero;

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
        //get player input
        float horizontal = Input.GetAxis("Horizontal");
        //check if the player is jumping
        bool isJumping = Input.GetButtonDown("Jump");
        
        //set move direction
        moveDirection = new Vector3(horizontal, 0, 0).normalized;

        //flip the player if they are moving in the opposite direction
        if (moveDirection.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
    }

    //FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        //check grounded by raycast
        isGrounded = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + 0.1f, LayerMask.GetMask("Ground"));
        //if the player can jump and is jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            //add force to the rigidbody
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        //set the x component of the rigidbody's velocity to the move direction multiplied by the move speed
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }
    
    #endregion

    #region custom methods
    //custom methods are methods that you create yourself go here

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

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
    public Healthbar healthbar;
    
    #endregion

    #region private variables
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;
    private BoxCollider2D boxCollider;
    private DissolveController dissolveController;
    private Vector2 moveDirection = Vector2.zero;
    private bool isJumping = false;
    private float boxColliderHeight;
    private float spriteHeight;

    

    //[serializefield] makes it visible in the inspector but the variable is still private
    [SerializeField] private float moveSpeed = 5f; 
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool isCrouching = false;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth = 100f;
    #endregion

    #region unity methods
    //Awake is called before Start
    void Awake()
    {
        //initialize the variables for the components attached to the object
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
        dissolveController = GetComponent<DissolveController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //initialize variables
        boxColliderHeight = boxCollider.size.y;
        spriteHeight = spriteRenderer.bounds.size.y;
        health = maxHealth;
        //healthbar.MaxHealth(maxHealth);
        dissolveController.isDissolving = false;

    }

    // Update is called once per frame
    void Update()
    {
        //get player input
        float horizontal = Input.GetAxisRaw("Horizontal");
        //check if the player is jumping
        isJumping = Input.GetButtonDown("Jump");
        //check grounded by raycast
        isGrounded = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + 0.1f, LayerMask.GetMask("Ground"));

        //if the player can jump and is jumping
        if (isGrounded && isJumping)
        {
            //make the player jump
            rb.velocity = Vector2.up * jumpForce;
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
        //check if the player is crouching
        isCrouching = Input.GetButton("Crouch");

        //check if the player is crouching
        if (isCrouching)
        {
            //set y scale to 0.5
            transform.localScale = new Vector2(transform.localScale.x, 0.3f/2f);
        }
        else
        {
            //set y scale to 1
            transform.localScale = new Vector2(transform.localScale.x, 0.3f);
        }
        
        //set move direction
        moveDirection = new Vector2(horizontal, 0).normalized;

        //change the animation state based on the player's movement
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        //flip the player if they are moving in the opposite direction
        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;  
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RemoveHealth(5.0f);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AddHealth(5.0f);
        }

        // Destroy charcter function
        if (health == 0)
        {
            Debug.Log("Player dead");
            // Add code to destory player when health is equal to 0
        }
    }

    //FixedUpdate is called once per physics update
    void FixedUpdate()
    {
       

        //if the player is crouching
        if (isCrouching)
        {
            //set the x component of the rigidbody's velocity to the move direction multiplied by the move speed
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
        }
        else
        {
            //set the x component of the rigidbody's velocity to the move direction multiplied by the move speed
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
        }

        
    }
    
    #endregion

    #region custom methods
    //custom methods are methods that you create yourself go here

    // Gets current health of player
    void GetHealth()
    {
        // Player health
    }
    // Removes health from player
    void RemoveHealth(float amount)
    {
        if (health == 0)
        {
            return;
        }
        else
        {
            health -= amount;
            healthbar.CurrentHealth(health);
        }
    }
    // Adds health to player
    void AddHealth(float amount)
    {
        if (health == maxHealth)
        {
            Debug.Log("Player is at max health");
        }
        else
        {
            health += amount;
            healthbar.CurrentHealth(health);
        }
    }
    #endregion

    #if UNITY_EDITOR
    #region debug methods
    //debug methods are methods that are only used for debugging and should be removed before the game is released

    
    //OnDrawGizmos is called every frame in the editor
    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            //draw a raycast to show where the player is grounded
            Gizmos.color = Color.red;
            Gizmos.DrawLine(boxCollider.bounds.center, new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.center.y - boxCollider.bounds.extents.y - 0.1f));

            //draw the collider size
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(boxCollider.bounds.center, new Vector2(boxCollider.size.x, boxCollider.size.y));
        }
    }
    
    #endregion
    #endif
}

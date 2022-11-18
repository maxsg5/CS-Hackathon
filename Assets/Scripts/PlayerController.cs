using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// 2D Platformer Controller
[RequireComponent(typeof(Rigidbody2D))] //[RequireComponent(typeof(capsuleCollider2D))] this tells unity to add a capsuleCollider to the object if it doesn't already have one
[RequireComponent(typeof(Animator))] 
[RequireComponent(typeof(AudioSource))] 
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    
    #region public variables
    //public variables are visible in the inspector by default
    //try to minimize the number of public variables and use private variables with public getters and setters instead when possible
    public bool isHoldingBox = false;
    public bool isFacingRight = true;
    public bool PlayDeadAnimation = false;
    #endregion

    #region private variables
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;
    private CapsuleCollider2D capsuleCollider;
    private DissolveController dissolveController;
    private Vector2 moveDirection = Vector2.zero;
    private bool isJumping = false;
    private float capsuleColliderHeight;
    private float spriteHeight;
    private Vector3 spawnPoint;
    private bool isCollidiingEnemy;


    //[serializefield] makes it visible in the inspector but the variable is still private
    [SerializeField] private float moveSpeed = 5f; 
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool isCrouching = false;

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
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        dissolveController = GetComponent<DissolveController>();
        spawnPoint = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        //initialize variables
        capsuleColliderHeight = capsuleCollider.size.y;
        spriteHeight = spriteRenderer.bounds.size.y;
        dissolveController.isDissolving = false;
    }

    // Update is called once per frame
    void Update()
    {
        //get player input
        float horizontal = Input.GetAxisRaw("Horizontal");
        //check if the player is jumping
        isJumping = Input.GetButtonDown("Jump");
        //check grounded by circlecast
        isGrounded = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - (spriteHeight / 2)), 0.3f, LayerMask.GetMask("Ground"));
        //if the player can jump and is jumping
        if (isGrounded && isJumping)
        {
            //make the player jump
            if (PlayDeadAnimation == false)
            {
                rb.velocity = Vector2.up * jumpForce;
                animator.SetBool("Jump", true);
            }
        }
        else
        {
            animator.SetBool("Jump", false);
        }
        //check if the player is crouching
        isCrouching = Input.GetButton("Crouch");

        //check if the player is crouching
        if (isCrouching && PlayDeadAnimation == false)
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
        if (PlayDeadAnimation == false)
        {
            moveDirection = new Vector2(horizontal, 0).normalized;
        } else
        {
            moveDirection = new Vector2(0, 0).normalized;
        }

        //change the animation state based on the player's movement
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        //flip the player if they are moving in the opposite direction
        if (horizontal > 0 && PlayDeadAnimation == false)
        {
            spriteRenderer.flipX = false;
            isFacingRight = true;
        }
        if (horizontal < 0 && PlayDeadAnimation == false)
        {
            spriteRenderer.flipX = true;
            isFacingRight = false;
        }

        if (GameManager.gameManager._playerHealth.Health <= 0 && PlayDeadAnimation == false)
        {
            PlayDeadAnimation = true;
            animator.SetTrigger("Dead");
            Respawn();
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

   public void Respawn()
    {
        //prevent player input
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        //fade out the player
        dissolveController.isDissolving = true;
        //wait for 1 second then move the player to the spawn point
        StartCoroutine(RespawnCoroutine());
        GameManager.gameManager.AddHealth(GameManager.gameManager._playerHealth.MaxHealth);

    }

    IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(3f);
        PlayDeadAnimation = false;
        // Reset position
        transform.position = spawnPoint;
        //fade in the player
        dissolveController.isDissolving = false;
        rb.isKinematic = false;
        animator.SetTrigger("Respawn");
    }

    //No use for player health custom commands in the player controller since its controlled by the game manager

    #endregion

    #if UNITY_EDITOR
    #region debug methods
    //debug methods are methods that are only used for debugging and should be removed before the game is released

    
    //OnDrawGizmos is called every frame in the editor
    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            //draw circle to show the grounded check
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y - (spriteHeight / 2)), 0.3f);

            //draw the collider size
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(capsuleCollider.bounds.center, new Vector2(capsuleCollider.size.x, capsuleCollider.size.y));
        }
    }
    
    #endregion
    #endif
}

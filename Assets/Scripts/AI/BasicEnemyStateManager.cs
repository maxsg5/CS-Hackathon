using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using AI;
namespace AI {
    public class BasicEnemyStateManager : MonoBehaviour
    {
        [Header("Current State")]
        public State currentState; //The current state of the enemy

        [Header("States")]
        public BasicEnemyPatrolState patrolState = new BasicEnemyPatrolState();
        public BasicEnemyChaseState chaseState = new BasicEnemyChaseState();
        public BasicEnemyAttackState attackState = new BasicEnemyAttackState();
        public BasicEnemyPeaceState peaceState = new BasicEnemyPeaceState();

        [Header("Patrol Variables")]
        public Transform target; //The player
        public Transform[] waypoints; //The points the enemy will patrol between
        public int waypointIndex = 0; //The current patrol point the enemy is moving towards
        public float moveSpeed = 1f; //The speed the enemy moves at
        public float PatrolWaitTime = 5f; //The amount of time the enemy will wait at the last patrol point before returning to patrol

        [Header("Chase Variables")]
        public float chaseSpeed = 10f; //The speed the enemy moves at when chasing the player
        public float chaseDistance = 10f; //The distance the enemy will chase the player from
        public float chaseTimer; //The timer for the chase wait time

        [Header("Attack Variables")]
        public float attackRange = 1f; //The range the enemy can attack the player from
        public float attackRate = 1f; //The rate at which the enemy can attack the player
        public float attackDamage = 10f; //The damage the enemy deals to the player
        public float attackTimer = 0f; //The timer for the attack rate
        public AudioClip attackSound; //The sound the enemy makes when attacking


        public Rigidbody2D rb; //The rigidbody of the enemy
        public Animator anim; // The animator of the enemy
        public SpriteRenderer sr; // The sprite renderer of the enemy
        public EnemyHealth enemyHealth;
        private USBDrop usbDrop;
        private BoxCollider2D collider;
        private AudioSource audioSource;
        public bool IsCollidingPlayer = false; // This bool is equal to if the enemy is colliding the player

        private bool isAttacking = false; //This bool is equal to if the enemy is attacking
        private bool AddingHealth = false;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();
            enemyHealth = GetComponent<EnemyHealth>();
            usbDrop = GetComponent<USBDrop>();
            collider = GetComponent<BoxCollider2D>();
            audioSource = GetComponent<AudioSource>();
        }

        // Start is called before the first frame update
        void Start()
        {
            //starting state for the FSM
            currentState = patrolState;
            //"this" is a reference to the current context (this EXACT monobehaviour script)
            currentState.Enter(this);

        }

        // Update is called once per frame
        void Update()
        {
            currentState.Update(this); //update the current state every frame
            // This is only for the attack state to apply damage
            if (currentState == attackState)
            {
                if(!isAttacking)
                {
                    isAttacking = true;
                    StartCoroutine(Attack());
                }
            }
            // Enemy does the peace animation when player dies
            if (GameManager.gameManager._playerHealth.Health <= 0)
            {
                SwitchState(peaceState);
            }
            if(IsCollidingPlayer == true && Input.GetMouseButtonDown(0))
            {
                enemyHealth.RemoveHealth(5f);
            }
            if (enemyHealth.Health <= 0)
            {
                StartCoroutine(Dead());
            }
            if(currentState == patrolState)
            {
                if (!AddingHealth)
                {
                    AddingHealth = true;
                    StartCoroutine(AddHealth());
                }
            }
        }

        public void SwitchState(State newState)
        {
            currentState = newState; //set the current state to the new state
            currentState.Enter(this); //enter the new state
        }

        public Rigidbody2D GetRigidbody2D()
        {
            return rb;
        }

        //This is take damage from the player. Should be synced with the punch and take into account attack rate
        IEnumerator Attack()
        {
            if (IsCollidingPlayer)
            {
                //play attack sound.
                audioSource.PlayOneShot(attackSound);
                GameManager.gameManager.RemoveHealth(attackDamage);
            }
            anim.SetBool("IsAttack", true);
            anim.speed = attackRate;
            yield return new WaitForSeconds(1.017f/ attackRate); //This is the length of the attack animation
            isAttacking = false;
            anim.SetBool("IsAttack", false);
        }

        IEnumerator Dead()
        {
            collider.size = new Vector2(0.9347277f, 0.6897885f);
            collider.offset = new Vector2(0.03151369f, 0.09204389f);
            anim.SetBool("IsDead", true);
            yield return new WaitForSeconds(0.25f);
            anim.SetBool("IsDead", false);
            Destroy(gameObject);
            usbDrop.Drop();
        }

        IEnumerator AddHealth()
        {
            enemyHealth.AddHealth(10f);
            yield return new WaitForSeconds(1f);
            AddingHealth = false;
        }

        public void Peace()
        {
            StartCoroutine(ShowPeace());
        }
        IEnumerator ShowPeace()
        {
            anim.SetBool("IsPeace", true);
            yield return new WaitForSeconds(3f);
            anim.SetBool("IsPeace", false);
            SwitchState(patrolState);
        }

        //Checking if the player is colliding with the Enemy
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                IsCollidingPlayer = true;
            }
        }

        // Checks if the player is no longer colliding with the Enemy
        public void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                IsCollidingPlayer = false;
            }
        }
#if UNITY_EDITOR
    //draw the radius for the chase distance in green
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
    
    //draw the radius for the attack range in red
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    }
#endif
}
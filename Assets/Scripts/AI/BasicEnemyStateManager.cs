using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using AI;
public class BasicEnemyStateManager : MonoBehaviour
{
    [Header("Current State")]
    public State currentState; //The current state of the enemy
    
    [Header("States")]
    public BasicEnemyPatrolState patrolState = new BasicEnemyPatrolState();
    public BasicEnemyChaseState chaseState = new BasicEnemyChaseState();
    
    [Header("Patrol Variables")]
    public Transform target; //The player
    public Transform[] waypoints; //The points the enemy will patrol between
    public int waypointIndex = 0; //The current patrol point the enemy is moving towards
    public float moveSpeed = 5f; //The speed the enemy moves at
    
    [Header("Chase Variables")]
    public float chaseSpeed = 10f; //The speed the enemy moves at when chasing the player
    public float chaseDistance = 10f; //The distance the enemy will chase the player from
    public float chaseWaitTime = 5f; //The amount of time the enemy will wait at the last patrol point before returning to patrol
    public float chaseTimer; //The timer for the chase wait time

    [Header("Attack Variables")]
    public float attackRange = 1f; //The range the enemy can attack the player from
    public float attackRate = 1f; //The rate at which the enemy can attack the player
    public float attackDamage = 10f; //The damage the enemy deals to the player
    public float attackTimer = 0f; //The timer for the attack rate
    
    
    private Rigidbody2D rb; //The rigidbody of the enemy


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //starting state for the FSM
        currentState = patrolState;
        //"this" is a reference to the current context (this EXACT monobehaviour script)
        currentState.EnterState(this);

    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this); //update the current state every frame
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        currentState.OnCollisionEnter2D(this, col);
    }

    public void SwitchState(State newState)
    {
        currentState = newState; //set the current state to the new state
        currentState.EnterState(this); //enter the new state
    }
    
    public Rigidbody2D GetRigidbody2D()
    {
        return rb;
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
    
    #endif
}

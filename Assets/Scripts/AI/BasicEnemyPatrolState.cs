using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace AI
{
    public class BasicEnemyPatrolState : State
    {
        public override void Enter(BasicEnemyStateManager stateManager)
        {
           // Logging that we enetered the Patrol state
           Debug.Log(stateManager + " Entering Patrol State");
           // Setting the anaimtion to walk
           stateManager.anim.SetBool("IsWalk", true);
           // Flipping the sprite to correct direction it is moving towards
            stateManager.sr.flipX = (stateManager.transform.position.x > stateManager.waypoints[stateManager.waypointIndex].position.x) ? true : false;
        }

        public override void Update(BasicEnemyStateManager stateManager)
        {

            // Moving the character to the next waypoint if its not already at it
            if (Vector2.Distance(stateManager.transform.position, stateManager.waypoints[stateManager.waypointIndex].position) > 0.1f)
            {
                stateManager.transform.position = Vector2.MoveTowards(stateManager.transform.position, stateManager.waypoints[stateManager.waypointIndex].position, stateManager.moveSpeed * Time.deltaTime);
            }
            else
            {   
                // Setting the next waypoint
                if (stateManager.waypointIndex + 1 >= stateManager.waypoints.Length)
                {
                    stateManager.waypointIndex = 0;
                }
                else
                {
                    stateManager.waypointIndex++;
                }
                // Flips the sprite to correct direction, then moves to next waypoint
                stateManager.sr.flipX = (stateManager.transform.position.x > stateManager.waypoints[stateManager.waypointIndex].position.x) ? true : false;
                stateManager.transform.position = Vector2.MoveTowards(stateManager.transform.position, stateManager.waypoints[stateManager.waypointIndex].position, stateManager.moveSpeed * Time.deltaTime);
            }
            
            //if the player is in range, switch to chase state
            if (Vector3.Distance(stateManager.transform.position, stateManager.target.transform.position) < stateManager.chaseDistance)
            {
                stateManager.SwitchState(stateManager.chaseState);
            }


        }
            
        public override void Exit(BasicEnemyStateManager stateManager)
        {
            // Have the waypoint set to the next for when this state is activated again, for smoother FSM cycle.
            if (stateManager.waypointIndex + 1 >= stateManager.waypoints.Length)
            {
                stateManager.waypointIndex = 0;
            }
            else
            {
                stateManager.waypointIndex++;
            }
        }
    }
}
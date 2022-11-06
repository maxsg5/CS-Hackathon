using UnityEngine;

namespace AI
{
    public class BasicEnemyPatrolState : State
    {
        public override void EnterState(BasicEnemyStateManager stateManager)
        {
            Debug.Log(stateManager + " Entering Patrol State");
        }

        public override void UpdateState(BasicEnemyStateManager stateManager)
        {
            //patrol along the waypoints
            stateManager.transform.position = Vector3.MoveTowards(stateManager.transform.position,
                stateManager.waypoints[stateManager.waypointIndex].position, Time.deltaTime * stateManager.moveSpeed);
            //if we reach the waypoint, move to the next waypoint
            if (Vector3.Distance(stateManager.transform.position,
                    stateManager.waypoints[stateManager.waypointIndex].position) < 0.1f)
            {
                stateManager.waypointIndex++;
                if (stateManager.waypointIndex >= stateManager.waypoints.Length)
                {
                    stateManager.waypointIndex = 0;
                }
            }

            //if the player is in range, switch to chase state
            if (Vector3.Distance(stateManager.transform.position, stateManager.target.transform.position) < stateManager.chaseDistance)
            {
                stateManager.SwitchState(stateManager.chaseState);
            }


        }

        public override void OnCollisionEnter2D(BasicEnemyStateManager stateManager, Collision2D collision)
        {
            Debug.Log("collided with " + collision.gameObject.name);
        }
        
        
       

    }
}
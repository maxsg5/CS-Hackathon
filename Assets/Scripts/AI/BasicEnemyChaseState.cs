using UnityEngine;

namespace AI
{
    public class BasicEnemyChaseState : State
    {
        public override void EnterState(BasicEnemyStateManager stateManager)
        {
            Debug.Log(stateManager + " Entered Chase State");
        }

        public override void UpdateState(BasicEnemyStateManager stateManager)
        {
            //chase the player
            Rigidbody2D rb = stateManager.GetRigidbody2D();
            
            Vector2 direction = stateManager.target.transform.position - stateManager.transform.position;
            direction.Normalize();
            rb.velocity = direction.normalized * stateManager.chaseSpeed;
            
            //if the player is within the attack range, transition to attack state
            if (Vector2.Distance(stateManager.transform.position, stateManager.target.transform.position) <= stateManager.attackRange)
            {
                //TODO: transition to attack state
            }

            //if the player is out of range, go back to patrol
            if (Vector3.Distance(stateManager.transform.position, stateManager.target.transform.position) > stateManager.chaseDistance)
            {
                stateManager.SwitchState(stateManager.patrolState);
            }
        
        }

        public override void OnCollisionEnter2D(BasicEnemyStateManager stateManager, Collision2D collision)
        {
            Debug.Log("collided with " + collision.gameObject.name);
        }
    }
}
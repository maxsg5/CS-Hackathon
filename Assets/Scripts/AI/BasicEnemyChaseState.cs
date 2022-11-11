using UnityEngine;

namespace AI
{
    public class BasicEnemyChaseState : State
    {
        //public MonoBehaviour monoBehaviour;
        public override void Enter(BasicEnemyStateManager stateManager)
        {
            Debug.Log(stateManager + " Entered Chase State");
            // Setting the walk animation
            stateManager.anim.SetBool("IsWalk", true);
            // Flipping the sprite to correct direction it is moving towards
            stateManager.sr.flipX = (stateManager.transform.position.x > stateManager.target.transform.position.x) ? true : false;
        }

        public override void Update(BasicEnemyStateManager stateManager)
        {   
            //This chases the player and flips the sprite accoring to the direction its facing/moving toward
            Vector2 direction = (stateManager.target.transform.position - stateManager.transform.position).normalized;
            stateManager.rb.velocity = new Vector2(direction.x * stateManager.chaseSpeed, stateManager.rb.velocity.y);
            stateManager.sr.flipX = (direction.x < 0) ? true : false;

            //if the player is within the attack range, transition to attack state
            if (Vector2.Distance(stateManager.transform.position, stateManager.target.transform.position) <= stateManager.attackRange)
            {
                stateManager.SwitchState(stateManager.attackState);
            }

            //if the player is out of range, go back to patrol
            if (Vector2.Distance(stateManager.transform.position, stateManager.target.transform.position) > stateManager.chaseDistance)
            {
                stateManager.SwitchState(stateManager.patrolState);
            }
        }
        public override void Exit(BasicEnemyStateManager stateManager)
        {
            Debug.Log("Chase state exit");
        }
    }
}
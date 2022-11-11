using UnityEngine;

namespace AI
{
    public class BasicEnemyAttackState : State
    {
        public override void Enter(BasicEnemyStateManager stateManager)
        {
            // Setting the inital animation and direction of enemy
            Debug.Log(stateManager + " Entered Attack State");
            stateManager.anim.SetBool("IsWalk", true);
            stateManager.anim.SetBool("IsAttack", true);
            stateManager.sr.flipX = (stateManager.transform.position.x > stateManager.target.transform.position.x) ? true : false;
        }
        public override void Update(BasicEnemyStateManager stateManager)
        {
            // If its out of the attack range, but in chase distance, go back to chase state
            if ((Vector2.Distance(stateManager.transform.position, stateManager.target.transform.position) > stateManager.attackRange) && (Vector2.Distance(stateManager.transform.position, stateManager.target.transform.position) <= stateManager.chaseDistance))
            {
                stateManager.anim.SetBool("IsAttack", false);
                stateManager.SwitchState(stateManager.chaseState);
            }

            // If its out of both attack range, and attack distance go back to patrol state (this should not be triggered, unless chasedistance and attackrance are the same)
            if ((Vector2.Distance(stateManager.transform.position, stateManager.target.transform.position) > stateManager.attackRange) && (Vector2.Distance(stateManager.transform.position, stateManager.target.transform.position) > stateManager.chaseDistance))
            {
                stateManager.anim.SetBool("IsAttack", false);
                stateManager.SwitchState(stateManager.patrolState);
            }

        }
        public override void Exit(BasicEnemyStateManager stateManager)
        {
            stateManager.anim.SetBool("IsAttack", false);
            Debug.Log("Attack state exit");
        }
    }
}
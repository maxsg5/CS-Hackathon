using UnityEngine;

namespace AI
{
    public class BasicEnemyPeaceState : State
    {
        public override void Enter(BasicEnemyStateManager stateManager)
        {
            // Setting the inital animation and direction of enemy
            Debug.Log(stateManager + " Entered Peace State");

            stateManager.Peace();
        }

        public override void Update(BasicEnemyStateManager stateManager){}


        public override void Exit(BasicEnemyStateManager stateManager)
        {
            stateManager.anim.SetBool("IsPeace", false);
            Debug.Log("Peace state exit");
        }
        
    }
}
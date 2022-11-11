using UnityEngine;

namespace AI
{
    public abstract class State
    {
        public abstract void Enter(BasicEnemyStateManager stateManager);
        public abstract void Update(BasicEnemyStateManager stateManager);
        public abstract void Exit(BasicEnemyStateManager stateManager);
    }
}
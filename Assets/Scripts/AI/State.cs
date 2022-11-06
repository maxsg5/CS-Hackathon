using UnityEngine;

public abstract class State 
{
    public abstract void EnterState(BasicEnemyStateManager stateManager);
    public abstract void UpdateState(BasicEnemyStateManager stateManager);
    public abstract void OnCollisionEnter2D(BasicEnemyStateManager stateManager, Collision2D collision);
}

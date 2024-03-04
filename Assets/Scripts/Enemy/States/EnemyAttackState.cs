using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyStateBase
{   
    [Header("EnemyAttackState")]
    [Header("Animations")]
    public string attackAnimationName;
    public bool isAttack;
    public string movementState;
    public override void StateEnter(StateParameter[] parameters = null)
    {
        StartCoroutine(ChangeState());
    }
    public override void StateExit()
    {
        
    }
    public override void StateLoop()
    {
       
    }
    public override void StatePhysicsLoop()
    {
        
    }
    public override void StateLateLoop()
    {
        
    }
    public override void StateInput()
    {
        
    }

    IEnumerator ChangeState()
    {
        enemyController.animator.Play(attackAnimationName);
        yield return new WaitForSeconds(1f);
        enemyController.isStateAttack = false;
        stateMachine.SetState(movementState);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementState : EnemyStateBase
{   
    [Header("EnemyMovementState")]
    [SerializeField] private float speed;
    [SerializeField] private float minDistance;
    [SerializeField] private float minYDistance;
    public Transform playerTarget;
    private Vector2 moveInput;
    private bool isFacingRight = true;
    [Header("Animations")]
    public string walkAnimationName; 
    public string idleAnimationName;
    public string attackState;
    public override void StateEnter(StateParameter[] parameters = null)
    {
        
    }
    public override void StateExit()
    {
        
    }
    public override void StateLoop()
    {
       Follow();
    }
    public override void StatePhysicsLoop()
    {
        
    }
    public override void StateLateLoop()
    {
        if (enemyController.isMoving)
        {
            enemyController.animator.Play(walkAnimationName);
        }
        else
        {
            enemyController.animator.Play(idleAnimationName);
        }
    }
    public override void StateInput()
    {
        
    }
    void Follow()
    {
        Vector2 enemyPosition = enemyController.movement.transform.position;
        Vector2 playerPosition = playerTarget.position;
        if (Mathf.Abs(enemyPosition.x - playerPosition.x) > minDistance)
        { 
            enemyPosition.x = Vector2.MoveTowards(enemyPosition, new Vector2(playerPosition.x, enemyPosition.y), 
                    speed * Time.deltaTime).x;
            enemyController.isMoving = true;
        }
        else
        {
            StartCoroutine(ChangeState());
        }
        if (Mathf.Abs(enemyPosition.y - playerPosition.y) > minYDistance)
        {
            enemyPosition.y = Vector2.MoveTowards(enemyPosition, new Vector2(enemyPosition.x, playerPosition.y), 
                    speed * Time.deltaTime).y;
            enemyController.isMoving = true;
        }
        enemyController.movement.transform.position = enemyPosition;
        bool isPlayerRight = enemyPosition.x < playerPosition.x;
        FlipEnemy(isPlayerRight);
    }
    void FlipEnemy(bool isPlayerRight)
    {
        if ((isFacingRight&&!isPlayerRight)||(!isFacingRight&& isPlayerRight))
        {
            isFacingRight=!isFacingRight;
            var transform1 = enemyController.transform;
            Vector3 scale = transform1.localScale;
            scale.x *= -1;
            transform1.localScale = scale;
        }
    }
    IEnumerator ChangeState()
    {
        enemyController.isMoving = false;
        yield return new WaitForSeconds(0.1f);
        stateMachine.SetState(attackState);
    }
}
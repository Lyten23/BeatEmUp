using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementState : EnemyStateBase
{   
    [Header("EnemyMovementState")]
    [SerializeField] private float speed;
    [SerializeField] private float minDistance;
    public Transform playerTarget;
    private bool isMoving;
    private Vector2 moveInput;
    private bool isFacingRight = true;
    [Header("Animations")]
    public string walkAnimationName; 
    public string idleAnimationName;

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
        if (!isMoving)
        {
            enemyController.animator.Play(idleAnimationName);
        }
        else
        {
            enemyController.animator.Play(walkAnimationName);
        }
    }
    public override void StateInput()
    {
        
    }
    void Follow()
    {
        if (Vector2.Distance(enemyController.movement.transform.position,playerTarget.position) > minDistance)
        {
            enemyController.movement.transform.position = Vector2.MoveTowards(enemyController.movement.transform.position, playerTarget.position, speed* Time.deltaTime);
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        bool isPlayerRight = enemyController.transform.position.x < playerTarget.transform.position.x;
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
}
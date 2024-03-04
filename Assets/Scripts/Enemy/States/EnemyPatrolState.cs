using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyStateBase
{
    [Header("EnemyPatrolState")] 
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float speed;
    public Transform playerTarget;
    private int currentWayPoint;
    public float waitTime;
    private bool isWaiting;
    public string chaseState;
    public float chaseDistance;
    [Header("Animations")]
    public string walkAnimation;
    public string idleAnimation;
    public override void StateEnter(StateParameter[] parameters = null)
    {
        
    }
    public override void StateExit()
    {
        
    }
    public override void StateLoop()
    {
        
        if (enemyController.transform.position != wayPoints[currentWayPoint].position)
        {
            enemyController.transform.position = Vector2.MoveTowards(enemyController.transform.position,
                wayPoints[currentWayPoint].position, speed * Time.deltaTime);
            enemyController.isMoving = true;
        }
        else if (!isWaiting)
        {
            enemyController.isMoving = false;
            StartCoroutine(Wait());
        }
        CheckChaseCondition();
    }

    private void CheckChaseCondition()
    {
        float distanceToPlayer = Vector2.Distance(enemyController.transform.position, playerTarget.position);
        
        if (distanceToPlayer <= chaseDistance)
        {
            stateMachine.SetState(chaseState);
        }
    }

    public override void StatePhysicsLoop()
    {
        
    }
    public override void StateLateLoop()
    {
        if (enemyController.isMoving)
        {
            enemyController.animator.Play(walkAnimation);
        }
        else
        {
            enemyController.animator.Play(idleAnimation);
        }
    }
    public override void StateInput()
    {
        
    }
    IEnumerator Wait()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        currentWayPoint++;
        if (currentWayPoint==wayPoints.Length)
        {
            currentWayPoint = 0;
        }
        isWaiting = false;
        Flip();
    }
    private void Flip()
    {
        if (enemyController.transform.position.x>wayPoints[currentWayPoint].position.x)
        {
            enemyController.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        { 
            enemyController.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
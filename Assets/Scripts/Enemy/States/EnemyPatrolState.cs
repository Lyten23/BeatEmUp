using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyStateBase
{
    [Header("EnemyPatrolState")] 
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float speed;
    private int currentWayPoint;
    public int waitTime;
    private bool isWaiting;
    [Header("Animations")]
    public string animationName;
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
        }
        else
        {
            StartCoroutine(Wait());
        }
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
    }
}
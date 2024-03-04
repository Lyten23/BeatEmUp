using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Variables
    [Header("References")] [Tooltip("Referencia del target del enemigo")] public Transform playerTarget;
    [Tooltip("Componente para aplicar movimiento al personaje")] public CharacterMovement movement;
    [Tooltip("Referencia al animator del personaje")] public Animator animator;
    [SerializeField] public bool isMoving;
    [SerializeField] public bool isStateAttack;
    public StateMachineController stateMachine;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float speed;
    [SerializeField] private int currentWayPoint;
    public int waitTime;
    private bool isWaiting;
    #endregion
    void Start()
    {
        stateMachine.Initialize();
    }
    void Update()
    {
        
      stateMachine.Step();
    } 
    private void FixedUpdate()
    { 
        stateMachine.PhysicsStep();
    }
    private void LateUpdate()
    { 
        stateMachine.LateLoop();
    }
    
}

using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class CharacterStateSimple1 : CharacterStateBase
{   
    [Header("SIMPLE STATE")]
    [Header("Animations")]
    public string animationName;
    [Header("Physics")]
    // Indicamos si debemos parar el movimiento al entrar
    public bool stopMovementOnEnter;
    // Indica si debemos reanudar el movimiento al salir
    public bool startMovementOnExit;
    [Header("Finish Conditions")] 
    // Indica si el estado termina con la animación o no.
    public bool exitOnAnimationEnds;
    // Tiempo de salida en caso de no salir con la animación
    public float timeToExit;
    [Header("States")] 
    // Nomvre del siguiente estado
    public string exitStateName;
    // Indica si la animación ha terminado
    private bool _animationHasEnded;
    // COntador de tiempo
    private float _timer;
    // Vector para almacenar la velocidad del personaje en caso de necesitarlo
    private Vector2 _movementBackup;
    public override void StateEnter(StateParameter[] parameters = null)
    {
        // Reproducimos la animación del estado.
        playerController.animator.Play(animationName);
        // Inicializmos las variables de control del estado.
        _animationHasEnded = false;
        _timer = 0;
        // Si debemos parar el movimiento al entrar, almacenamos el movimiento en nuestra vraiable par ello, y lo paramos.
        if (stopMovementOnEnter)
        {
            _movementBackup = playerController.movement.GroundVelocity;
            playerController.movement.Move(0f,0f);
        }
    }
    public override void StateExit()
    {
        // Si debemos parar el movimiento al salir...
        if (stopMovementOnEnter && startMovementOnExit)
        {
            playerController.movement.Move(_movementBackup.x,_movementBackup.y);
        }
    }
    public override void StateLoop()
    {
        if (!exitOnAnimationEnds)
        {
            _timer += Time.deltaTime;
        }
    }
    public override void StatePhysicsLoop()
    {
        
    }
    public override void StateLateLoop()
    {
        if (exitOnAnimationEnds&&!playerController.animator.IsPlaying(animationName))
        {
            _animationHasEnded = true;
        }
    }
    public override void StateInput()
    {
        if ((!exitOnAnimationEnds && _timer >= timeToExit || _animationHasEnded))
        {
            stateMachine.SetState(exitStateName);
        }
    }
    // private bool = _active; 
    // void Awake() => Invoke(nameof(Activate), 0.5f);
    // void Activate() =>  _active = true;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterStateHit : CharacterStateBase
{   
    [Header("Hit State")]
    [Header("Animations")]
    public string animationName;
    [Header("Configuration")]
    // Tiempo que dura el estado (incapacitación)
    public float time;
    public string endStateName;
    [Range(0,1)]
    public float shakeIntensity = 0.5f;
    // Variación máxima del shake
    private float _shakeMaxVariation = 0.05f;
    // Variación que se aplica en el shake
    private float _shakeVariation;
    // Contador de tiempo del estado
    private float _timer;

    private HitManager.HitInfo _currentHitInfo;
    public override void StateEnter(StateParameter[] parameters = null)
    {
        _timer = 0;
        _shakeVariation = shakeIntensity * _shakeVariation;
        playerController.animator.Play(animationName);
       
        if (parameters!=null && parameters.Length>0)
        {
            if(parameters[0].value is not HitManager.HitInfo) return;
            // Obtenemos el parámetro como un HitInfo y lo almacenamos en la variable de hit dle estado.
            _currentHitInfo = (HitManager.HitInfo) parameters[0].value;
            // Asignamos la fuerza del hit al personaje.
            playerController.movement.SetVelocity(_currentHitInfo.groundForce,_currentHitInfo.verticalForce);
        }
    }
    public override void StateExit()
    {
        StopShake();
    }
    public override void StateLoop()
    {
        _timer += Time.deltaTime;
    }
    public override void StatePhysicsLoop()
    {
        
    }
    public override void StateLateLoop()
    {
        Shake();
    }
    public override void StateInput()
    {
        if (_timer>=time)
        {
            stateMachine.SetState(endStateName);
            return;
        }
    }

    private void Shake()
    {
        // Obtenemos la posición local del body del personaje
        Vector2 currentBodyPosition = playerController.movement.characterBody.localPosition;
        // Aplicamos la variación en X. Si está a la derecha lo colocamos a la izquierad y ciceversa
        currentBodyPosition.x = currentBodyPosition.x > 0 ? -_shakeVariation : _shakeMaxVariation;
        // Reasignamos la nueva posición local. 
        playerController.movement.characterBody.localPosition = currentBodyPosition;
    }
    private void StopShake()
    {
        // Obtenemos la posición local del body del personaje
        Vector2 currentBodyPosition = playerController.movement.characterBody.localPosition;
        // Reseteamos la posición local en X
        currentBodyPosition.x = 0;
        // Reasignamos la nueva posición local. 
        playerController.movement.characterBody.localPosition = currentBodyPosition;
    }
}
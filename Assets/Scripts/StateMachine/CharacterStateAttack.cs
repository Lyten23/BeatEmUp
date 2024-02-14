using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateAttack : CharacterStateBase
{   
    [Header("AttackState")]
    [Header("Animations")]
    public string animationName;
    // Estado al que transiciona al terminar el ataque
    public string endStateName;
    // Area de daño del ataque
    public Bounds hitBox;
    // Intervalo de frames en los que el ataque hará daño
    public int startHitFrame;
    public int endHitFrame;
    // Estado del siguiente ataque
    public string nextAttackState;
    // Variable de control que indica si el hit está activo
    [Header("hit Info")] public HitManager.HitInfo hitInfo;
    protected bool hitIsActive;
    // Variable de control que indica si se ha solicitado conexión con el siguiente ataque
    protected bool hasConnect;
    // Variable de control que indica si hay hit
    protected bool hasHit;
    // Indica el frame actual de la animación
    protected int currentFrame;
    // Indica el total de frames de la animación
    protected int totalFrames;
    [Header("Particulas")] 
    public ParticleSystem [] particleSystem;

    private void OnDrawGizmos()
    {
        Gizmos.color=Color.cyan;
        Gizmos.DrawWireCube(playerController.transform.position + hitBox.center, hitBox.size);
    }

    public override void StateEnter(StateParameter[] parameters = null)
    {
        // Paramos el movimiento
        playerController.movement.Move(0,0);
        // Inicializamos las variables de control
        hitIsActive = false;
        hasConnect = false;
        hasHit = false;
        currentFrame = 0;
        
        playerController.animator.Play(animationName,-1,0);
    }
    public override void StateExit()
    {
        
    }
    public override void StateLoop()
    {
        if (hitIsActive)
        {
            // TODO: Ejecutamos el método de hit
            PerformHit();
        }
    }
    public override void StatePhysicsLoop()
    {
        
    }
    public override void StateLateLoop()
    {
        // Obtenemos el número de frames de la animación y el frame actual
        totalFrames = playerController.animator.GetTotalFrames(animationName);
        currentFrame = playerController.animator.GetCurrentFrame(animationName);
        // Comprobamos si estamos en intervalo de hit o no
        if (!hitIsActive && currentFrame>=startHitFrame && (currentFrame<endHitFrame || startHitFrame>endHitFrame))
        {
            // Si el frame actual está por encima de inicio del hit y por debajo del final del hit o el frame inicial es mayor que el final (para casos en los que no usemos el frame final)
            hitIsActive = true;
        }
        else if (hitIsActive && (currentFrame < startHitFrame || (currentFrame > endHitFrame && currentFrame >= endHitFrame)))
        {
            // Si está fuera del intervalo de ataque
            hitIsActive = false;
        }
        // Si la animación no ha terminado, cortamos la ejecución del método
        if (currentFrame<totalFrames)return;
        // Si ha conectado con el siguiente ataque y hay estado de ataque configurado...
        if (hasConnect && !string.IsNullOrEmpty(nextAttackState))
        {
            // y, además hay hit...
            if (hasHit)
            {
                // Ejecutamos el siguiente ataque
                stateMachine.SetState(nextAttackState);
                foreach (var t in particleSystem)
                {
                    t.Play();
                }
            }
            else
            {
                // Si no hay hit ejecutamos este mismo ataque
                stateMachine.SetState(stateName);
            }
        } else if (!string.IsNullOrEmpty(endStateName))
        {
            // Si no se conecta con otro ataque y hay estado de fin, pasamos a este.
            foreach (var t in particleSystem)
            {
                t.Play();
            }
            stateMachine.SetState(endStateName);
        }
    }
    public override void StateInput()
    {
        // Si el jugador pulsa el botón de ataque
        if (Input.GetButtonDown("Fire1"))
        {
            // Indicamos que hay que conectar con el siguiente ataque
            hasConnect = true;
        }
    }
    /// <summary>
    /// Método que lanza el hit del ataque
    /// </summary>
    protected void PerformHit()
    {
        Vector2 hitBoxCenter = hitBox.center;
        hitBoxCenter.x = playerController.movement.FaceDirection.x > 0
            ? MathF.Abs(hitBox.center.x)
            : -MathF.Abs(hitBox.center.x);
        hitInfo.groundForce.x = playerController.movement.FaceDirection.x > 0
            ? MathF.Abs(hitInfo.groundForce.x)
            : -MathF.Abs(hitInfo.groundForce.x);
        hitBox.center = hitBoxCenter;
        var position = playerController.transform.position;
        Collider2D[] others = Physics2D.OverlapAreaAll(position + hitBox.min,position + hitBox.max);
        if (others == null) return;
        for (int i = 0; i < others.Length; i++)
        {
            // Comprobamos si el hit es válido
            bool result = false;
            if (others[i].TryGetComponent(out HitReceiver hitReceiver))
            {
                result = HitManager.Instance.CheckHit(playerController.transform, hitReceiver, hitInfo);
            }
            // Si ya hay hit, lo dejamos como está.
            if (!hasHit)
            {
                hasHit = result;
            }
        }
    }
}
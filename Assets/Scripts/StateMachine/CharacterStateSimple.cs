using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Programa un nuevo estado llamado CharacterStatesSimple que pueda realizar las acciones listadas a continuación:
//Parar el movimiento al entrar.
//Reanudar el movimiento al salir.
//Tiempo para salir.
//Estado de salida.
public class CharacterStateSimple : CharacterStateBase
{   
    [Header("CharacterStateSimple")]
    [Header("Animations")]
    public string animationName;
    public bool isJump; 
    public override void StateEnter(StateParameter[] parameters = null)
    {
        
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
}
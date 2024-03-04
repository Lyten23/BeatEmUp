using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFallDownState : CharacterStateBase
{   
    [Header("CharacterFallDownState")]
    [Header("Animations")]
    public string animationName;

    public string recoveryState;
    public override void StateEnter(StateParameter[] parameters=null)
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

    IEnumerator ChangeState()
    {
        playerController.animator.Play(animationName);
        yield return new WaitForSeconds(1);
        stateMachine.SetState(recoveryState);
    }
}
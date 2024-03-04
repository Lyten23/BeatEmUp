using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRecoveryState : CharacterStateBase
{   
    [Header("CharacterRecoveryState")]
    [Header("Animations")]
    public string animationName;

    public string exitState;
    public override void StateEnter(StateParameter[] parameters = null)
    {
        playerController.animator.Play(animationName);
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
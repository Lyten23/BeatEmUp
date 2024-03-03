using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class CharacterStateBase : StateBase
{
    public PlayerController playerController;
    #region StateBase
    public abstract override void StateEnter(StateParameter[] parameters = null);
    public abstract override void StateExit();
    public abstract override void StateLoop();
    public abstract override void StatePhysicsLoop();
    public abstract override void StateLateLoop();
    public abstract override void StateInput();
    #endregion
}

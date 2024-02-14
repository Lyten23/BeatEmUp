using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReceiver : MonoBehaviour
{
    public StateMachineController stateMachine;
    public string hitStateName;
    [ContextMenu("Hit")]
    public void Hit(HitManager.HitInfo hitInfo)
    {
        //Creamos el parámetro del hit info
        StateBase.StateParameter parameter = new StateBase.StateParameter()
        {
            value = hitInfo
        };
        // Creamos un array de parametros e insertamos el parametro anterior
        StateBase.StateParameter[] parameters = new StateBase.StateParameter[] { parameter };
        stateMachine.SetState(hitStateName,parameters);
    }
}

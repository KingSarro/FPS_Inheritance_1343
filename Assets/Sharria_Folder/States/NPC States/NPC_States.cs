using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC_States : StateCore{

    [Header("NPC States")] //Displays a header on the inspector
    [SerializeField] State_NPC_Attack attack;
    [SerializeField] State_NPC_Wander wander;
    [SerializeField] State_NPC_Pursue pursue;
    [SerializeField] State_NPC_Recovery recovery;

    private void Start(){
        Setup_States(); //Sets up the core for this state machine
        sMachine.ChangeState(wander);
    }
    private void Update(){
        //Updates the current state
        sMachine.currentState.Update_State();
    }
}

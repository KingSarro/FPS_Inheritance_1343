using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class StateCore : MonoBehaviour{
    [Header("Physics Components")] //Displays a header on the inspector
    ////public Rigidbody rBody;
    //This puts a tooltip on the collider reference 
    [Tooltip("This can be any Collider, I just put cap in front so it isn't trying to use a keyword")]
    public Collider capCollider;
    protected static StateMachine sMachine;
    public NavMeshAgent agent;
    public float moveSpeed;

    public void Setup_States(){
        //Creates a new instance of the state machine
        sMachine = new StateMachine();
        //Gets a reference to the parent object's NavMeshAgent component
        agent = GetComponentInChildren<NavMeshAgent>();

        //Looks for all the child objects that are of class State and saves to an array of Class State
        State[] allChildStates = GetComponentsInChildren<State>(); 
        //Loops through all the child states...
        foreach (State state in allChildStates){
            //..and sets their core to be this instance of the script
            state.SetCore(this);
        }
    }
}
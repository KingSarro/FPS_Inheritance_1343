using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class StateMachine{
    public State currentState; //Creates a reference to hold the instance of the current state

    public void ChangeState(State newState, bool forceReset = false){
        //If the current state is differnt from the new state, or the forceReset is not set to true...
        if(currentState != newState || forceReset){
            //End the current state
            currentState?.Exit_State();
            //Switch the current state to the new state
            currentState = newState;
            ////Switch the current state to the new state
            ////Setup_States();
            //Start the new state
            currentState?.Enter_State();
        }
    }
}

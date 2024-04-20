using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State: MonoBehaviour{

    StateCore sCore;
    public bool isComplete = false;
    //public bool isOnCooldown = false;

    public virtual void Enter_State(){}
    public virtual void Update_State(){}
    public virtual void Exit_State(){}

    public void SetCore(StateCore core){
        sCore = core;
    }
}
